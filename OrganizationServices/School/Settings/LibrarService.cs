using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto.Settings;
using OrganizationIInterface.IService.School.Settings;
using OrganizationModels.Model.Settings;

namespace OrganizationServices.School.Settings
{
    public class LibrarService : ILibraryInterfaceService
    {
        private readonly IUnitOfWork _Unit;
        private readonly ILogger<LibrarService> _Logger;
        private readonly IMapper _Mapper;

        public LibrarService(IMapper mapper,
                             ILogger<LibrarService> logger,
                             IUnitOfWork unit)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public async Task<bool> AddABookAsync(LibraryItemsDto library)
        {
            try
            {
                var book = await _Unit.Library.GetBookByTitleAndAuthorAndYear(library.Title!, library.Author!, library.Year!);

                if (book != null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The book {library.Title} by {library.Author} for the year {library.Year} already exist");
                }

                var libaryItem = _Mapper.Map<LibraryItem>(library);

                libaryItem.LibraryId = Guid.NewGuid();
                libaryItem.CreatedAt = DateTime.Now;
                libaryItem.UpdatedAt = DateTime.Now;

                await _Unit.Library.AddAsync(libaryItem);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception.Message, "An error occurred while adding a book to the library.");
                throw;
            }
        }

        public async Task<bool> DeleteABookItemAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The library item Id cannot be empty");
                }

                var book = await _Unit.Library.GetByIdAsync(id);

                if (book == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"No library item found with the Id: {id}");
                }

                _Unit.Library.Delete(book);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception.Message, "An error occurred while deleting a library item.");
                throw;
            }
        }

        public async Task<LibraryItemsDto> GetABookItemByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The library item Id cannot be empty");
                }

                var book = await _Unit.Library.GetByIdAsync(id);

                if (book == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"No library item found with the Id: {id}");
                }

                return _Mapper.Map<LibraryItemsDto>(book);
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception.Message, "An error occurred while retrieving a library item by Id.");
                throw;
            }
        }

        public Task<IEnumerable<LibraryItemsDto>> GetAllLibraryItemsAsync()
        {
            try
            {
                var books =  _Unit.Library.GetAllAsync().Result;

                return Task.FromResult(_Mapper.Map<IEnumerable<LibraryItemsDto>>(books));
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception.Message, "An error occurred while retrieving all library items.");
                throw;
            }
        }

        public Task<bool> UpdateABookAsync(Guid id, LibraryItemsDto library)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The library item Id cannot be empty");
                }

                var book = _Unit.Library.GetByIdAsync(id).Result;

                if (book == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"No library item found with the Id: {id}");
                }

                _Mapper.Map(library, book);

                book.UpdatedAt = DateTime.Now;

                _Unit.Library.Update(book);

                _Unit.SaveChangeAsync().Wait();

                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception.Message, "An error occurred while updating a library item.");
                throw;
            }
        }
    }
}
