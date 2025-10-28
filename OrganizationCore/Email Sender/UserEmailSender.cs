
using Microsoft.Extensions.Logging;
using OrganizationDTO.Dto;

namespace OrganizationCore.Email_Sender
{
    public class UserEmailSender : IUserEmailSenderInterface
    {
        private readonly IEmailSender _EmailSender;
        private readonly ILogger<UserEmailSender> _Logger;

        public UserEmailSender(IEmailSender emailSender,
                              ILogger<UserEmailSender> logger)
        {
            _EmailSender = emailSender;
            _Logger = logger;
        }

        public async Task<bool> AdminEmailSender(EmailSenderDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _Logger.LogWarning("EmailSenderDto is null.");

                    return false;
                }

                string emailBody = $@"
                <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            * {{
                                margin: 0;
                                padding: 0;
                                box-sizing: border-box;
                            }}
                            body {{
                                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%);
                                padding: 40px 20px;
                            }}
                            .email-container {{
                                max-width: 600px;
                                margin: 0 auto;
                                background: #ffffff;
                                border-radius: 16px;
                                overflow: hidden;
                                box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
                            }}
                            .header {{
                                background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%);
                                padding: 50px 30px;
                                text-align: center;
                                position: relative;
                            }}
                            .admin-badge {{
                                background: #ffd700;
                                color: #1e3c72;
                                padding: 8px 20px;
                                border-radius: 20px;
                                font-size: 12px;
                                font-weight: 700;
                                text-transform: uppercase;
                                letter-spacing: 1px;
                                display: inline-block;
                                margin-bottom: 20px;
                            }}
                            .header img {{
                                max-width: 100px;
                                margin-bottom: 20px;
                                border-radius: 12px;
                                background: white;
                                padding: 10px;
                            }}
                            .header h1 {{
                                color: #ffffff;
                                font-size: 32px;
                                font-weight: 700;
                                margin: 0;
                            }}
                            .content {{
                                padding: 40px 30px;
                                color: #333333;
                            }}
                            .greeting {{
                                font-size: 26px;
                                font-weight: 700;
                                color: #1e3c72;
                                margin-bottom: 20px;
                            }}
                            .text {{
                                font-size: 16px;
                                line-height: 1.8;
                                color: #555555;
                                margin-bottom: 15px;
                            }}
                            .admin-panel {{
                                background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);
                                border-left: 4px solid #1e3c72;
                                border-radius: 8px;
                                padding: 25px;
                                margin: 30px 0;
                            }}
                            .admin-title {{
                                font-size: 18px;
                                font-weight: 700;
                                color: #1e3c72;
                                margin-bottom: 15px;
                            }}
                            .permission-item {{
                                display: flex;
                                align-items: center;
                                margin-bottom: 12px;
                                font-size: 15px;
                            }}
                            .permission-icon {{
                                width: 30px;
                                height: 30px;
                                background: #1e3c72;
                                border-radius: 6px;
                                display: flex;
                                align-items: center;
                                justify-content: center;
                                color: white;
                                font-weight: bold;
                                margin-right: 12px;
                                font-size: 16px;
                            }}
                            .button-container {{
                                text-align: center;
                                margin: 35px 0;
                            }}
                            .button {{
                                display: inline-block;
                                background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%);
                                color: #ffffff;
                                padding: 18px 45px;
                                text-decoration: none;
                                border-radius: 30px;
                                font-size: 16px;
                                font-weight: 600;
                                box-shadow: 0 8px 20px rgba(30, 60, 114, 0.4);
                                transition: transform 0.3s ease;
                            }}
                            .security-notice {{
                                background: #fff3cd;
                                border-left: 4px solid #ffc107;
                                padding: 20px;
                                border-radius: 8px;
                                margin: 25px 0;
                            }}
                            .security-notice strong {{
                                color: #856404;
                                display: block;
                                margin-bottom: 8px;
                            }}
                            .security-notice p {{
                                color: #856404;
                                font-size: 14px;
                                margin: 0;
                            }}
                            .footer {{
                                background: #1e3c72;
                                padding: 30px;
                                text-align: center;
                                color: #ffffff;
                                font-size: 13px;
                                line-height: 1.6;
                            }}
                            .footer-links {{
                                margin-top: 15px;
                            }}
                            .footer-link {{
                                color: #ffd700;
                                text-decoration: none;
                                margin: 0 10px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <div class='header'>
                                <div class='admin-badge'>🛡️ Administrator Access</div>
                                <img src='https://media.licdn.com/dms/image/v2/D4D0BAQE_Sg1d16i2Eg/company-logo_200_200/company-logo_200_200/0/1714025968860/thutonet_logo?e=1749081600&v=beta&t=Ksu1go1Z71gXBrcRTRtpC-l5mPCYH64GGnlV49mlQHY' alt='ThutoNet Logo' />
                                <h1>Admin Account Created</h1>
                            </div>
                            <div class='content'>
                                <div class='greeting'>Welcome, {dto.FirstName} {dto.LastName}</div>
                                <p class='text'>Your administrator account for ThutoNet has been successfully created. You now have full access to manage and oversee the platform.</p>
                                
                                <div class='admin-panel'>
                                    <div class='admin-title'>Your Administrative Privileges</div>
                                    <div class='permission-item'>
                                        <div class='permission-icon'>👥</div>
                                        <div>Full user management and role assignment</div>
                                    </div>
                                    <div class='permission-item'>
                                        <div class='permission-icon'>⚙️</div>
                                        <div>System configuration and settings control</div>
                                    </div>
                                    <div class='permission-item'>
                                        <div class='permission-icon'>📊</div>
                                        <div>Access to analytics and reporting dashboards</div>
                                    </div>
                                    <div class='permission-item'>
                                        <div class='permission-icon'>🔐</div>
                                        <div>Security and access control management</div>
                                    </div>
                                    <div class='permission-item'>
                                        <div class='permission-icon'>📝</div>
                                        <div>Content moderation and approval workflows</div>
                                    </div>
                                </div>
                    
                                <p class='text'>To activate your administrator account, please verify your email address:</p>
                                
                                <div class='button-container'>
                                    <a href='{dto.CallBackUrl}' class='button'>Verify Admin Account</a>
                                </div>
                    
                                <div class='security-notice'>
                                    <strong>🔒 Security Reminder</strong>
                                    <p>As an administrator, you have elevated privileges. Please ensure you use a strong password, enable two-factor authentication when available, and never share your credentials.</p>
                                </div>
                                
                                <p class='text' style='font-size: 14px; color: #888;'>If you did not request this administrator account, please contact our security team immediately.</p>
                            </div>
                            <div class='footer'>
                                <p><strong>ThutoNet Administration</strong></p>
                                <p>Secure. Reliable. Powerful.</p>
                                <div class='footer-links'>
                                    <a href='#' class='footer-link'>Admin Documentation</a> |
                                    <a href='#' class='footer-link'>Security Center</a> |
                                    <a href='#' class='footer-link'>Support</a>
                                </div>
                                <p style='margin-top: 15px;'>&copy; {DateTime.UtcNow.Year} ThutoNet. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                await _EmailSender.SendEmailAsync(dto.Email, "Confirmation Email", emailBody);

                return true;
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception, "Error occurred while sending admin email.");

                return false;
            }
        }

        public async Task<bool> GuestEmailSender(EmailSenderDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _Logger.LogWarning("EmailSenderDto is null.");

                    return false;
                }

                string emailBody = $@"
                <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            * {{
                                margin: 0;
                                padding: 0;
                                box-sizing: border-box;
                            }}
                            body {{
                                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                background: linear-gradient(135deg, #a8edea 0%, #fed6e3 100%);
                                padding: 40px 20px;
                            }}
                            .email-container {{
                                max-width: 600px;
                                margin: 0 auto;
                                background: #ffffff;
                                border-radius: 16px;
                                overflow: hidden;
                                box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
                            }}
                            .header {{
                                background: linear-gradient(135deg, #a8edea 0%, #fed6e3 100%);
                                padding: 50px 30px;
                                text-align: center;
                            }}
                            .guest-badge {{
                                background: #ffffff;
                                color: #5f9ea0;
                                padding: 8px 20px;
                                border-radius: 20px;
                                font-size: 12px;
                                font-weight: 700;
                                text-transform: uppercase;
                                letter-spacing: 1px;
                                display: inline-block;
                                margin-bottom: 20px;
                            }}
                            .header img {{
                                max-width: 110px;
                                margin-bottom: 20px;
                                border-radius: 12px;
                                background: white;
                                padding: 10px;
                            }}
                            .header h1 {{
                                color: #333333;
                                font-size: 30px;
                                font-weight: 700;
                                margin: 0;
                            }}
                            .content {{
                                padding: 40px 30px;
                                color: #333333;
                            }}
                            .greeting {{
                                font-size: 26px;
                                font-weight: 700;
                                color: #5f9ea0;
                                margin-bottom: 20px;
                            }}
                            .text {{
                                font-size: 16px;
                                line-height: 1.8;
                                color: #555555;
                                margin-bottom: 15px;
                            }}
                            .explore-section {{
                                background: linear-gradient(135deg, #f0f9ff 0%, #fff0f6 100%);
                                border-radius: 12px;
                                padding: 30px;
                                margin: 30px 0;
                            }}
                            .explore-title {{
                                font-size: 20px;
                                font-weight: 700;
                                color: #5f9ea0;
                                margin-bottom: 20px;
                                text-align: center;
                            }}
                            .feature-grid {{
                                display: grid;
                                grid-template-columns: 1fr 1fr;
                                gap: 15px;
                                margin-top: 20px;
                            }}
                            .feature-box {{
                                background: white;
                                padding: 20px;
                                border-radius: 10px;
                                text-align: center;
                                box-shadow: 0 2px 8px rgba(95, 158, 160, 0.1);
                            }}
                            .feature-emoji {{
                                font-size: 32px;
                                margin-bottom: 10px;
                            }}
                            .feature-name {{
                                font-size: 14px;
                                font-weight: 600;
                                color: #333;
                            }}
                            .button-container {{
                                text-align: center;
                                margin: 35px 0;
                            }}
                            .button {{
                                display: inline-block;
                                background: linear-gradient(135deg, #5f9ea0 0%, #88c0d0 100%);
                                color: #ffffff;
                                padding: 18px 45px;
                                text-decoration: none;
                                border-radius: 30px;
                                font-size: 16px;
                                font-weight: 600;
                                box-shadow: 0 8px 20px rgba(95, 158, 160, 0.3);
                                transition: transform 0.3s ease;
                            }}
                            .info-box {{
                                background: #e8f5e9;
                                border-left: 4px solid #4caf50;
                                padding: 20px;
                                border-radius: 8px;
                                margin: 25px 0;
                            }}
                            .info-box strong {{
                                color: #2e7d32;
                                display: block;
                                margin-bottom: 8px;
                            }}
                            .info-box p {{
                                color: #2e7d32;
                                font-size: 14px;
                                margin: 0;
                            }}
                            .footer {{
                                background: #f8f9fa;
                                padding: 30px;
                                text-align: center;
                                color: #888888;
                                font-size: 13px;
                                line-height: 1.6;
                            }}
                            .footer-links {{
                                margin-top: 15px;
                            }}
                            .footer-link {{
                                color: #5f9ea0;
                                text-decoration: none;
                                margin: 0 10px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <div class='header'>
                                <div class='guest-badge'>🌐 Guest Access</div>
                                <img src='https://media.licdn.com/dms/image/v2/D4D0BAQE_Sg1d16i2Eg/company-logo_200_200/company-logo_200_200/0/1714025968860/thutonet_logo?e=1749081600&v=beta&t=Ksu1go1Z71gXBrcRTRtpC-l5mPCYH64GGnlV49mlQHY' alt='ThutoNet Logo' />
                                <h1>Welcome to ThutoNet!</h1>
                            </div>
                            <div class='content'>
                                <div class='greeting'>Hi {dto.FirstName} {dto.LastName}! 👋</div>
                                <p class='text'>Thanks for your interest in ThutoNet! We've created a guest account for you to explore our platform and discover what we have to offer.</p>
                                
                                <div class='explore-section'>
                                    <div class='explore-title'>What You Can Explore</div>
                                    <p class='text' style='text-align: center; font-size: 15px; color: #666; margin-bottom: 20px;'>As a guest, you have access to preview our platform features:</p>
                                    <div class='feature-grid'>
                                        <div class='feature-box'>
                                            <div class='feature-emoji'>📚</div>
                                            <div class='feature-name'>Browse Courses</div>
                                        </div>
                                        <div class='feature-box'>
                                            <div class='feature-emoji'>🎥</div>
                                            <div class='feature-name'>Watch Demos</div>
                                        </div>
                                        <div class='feature-box'>
                                            <div class='feature-emoji'>💡</div>
                                            <div class='feature-name'>View Resources</div>
                                        </div>
                                        <div class='feature-box'>
                                            <div class='feature-emoji'>🌟</div>
                                            <div class='feature-name'>Explore Features</div>
                                        </div>
                                    </div>
                                </div>
                    
                                <p class='text'>To start exploring, please verify your email address:</p>
                                
                                <div class='button-container'>
                                    <a href='{dto.CallBackUrl}' class='button'>Verify & Start Exploring</a>
                                </div>
                    
                                <div class='info-box'>
                                    <strong>💎 Ready for More?</strong>
                                    <p>Upgrade to a full account anytime to unlock complete access to all courses, assignments, certifications, and community features!</p>
                                </div>
                                
                                <p class='text' style='font-size: 14px; color: #888;'>We hope you enjoy exploring ThutoNet. If you have any questions, feel free to reach out!</p>
                            </div>
                            <div class='footer'>
                                <p><strong>ThutoNet</strong></p>
                                <p>Discover. Learn. Grow.</p>
                                <div class='footer-links'>
                                    <a href='#' class='footer-link'>Explore Courses</a> |
                                    <a href='#' class='footer-link'>Upgrade Account</a> |
                                    <a href='#' class='footer-link'>Help Center</a>
                                </div>
                                <p style='margin-top: 15px;'>&copy; {DateTime.UtcNow.Year} ThutoNet. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                await _EmailSender.SendEmailAsync(dto.Email, "Confirmation Email", emailBody);

                return true;
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception, "Error occurred while sending guest email.");

                return false;
            }
        }

        public async Task<bool> LearnerEmailSender(EmailSenderDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _Logger.LogWarning("EmailSenderDto is null.");
                    return false;
                }

                string emailBody = $@"
                   <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            * {{
                                margin: 0;
                                padding: 0;
                                box-sizing: border-box;
                            }}
                            body {{
                                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
                                padding: 40px 20px;
                            }}
                            .email-container {{
                                max-width: 600px;
                                margin: 0 auto;
                                background: #ffffff;
                                border-radius: 16px;
                                overflow: hidden;
                                box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
                            }}
                            .header {{
                                background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
                                padding: 50px 30px;
                                text-align: center;
                                position: relative;
                            }}
                            .learner-badge {{
                                background: #ffffff;
                                color: #f5576c;
                                padding: 8px 20px;
                                border-radius: 20px;
                                font-size: 12px;
                                font-weight: 700;
                                text-transform: uppercase;
                                letter-spacing: 1px;
                                display: inline-block;
                                margin-bottom: 20px;
                            }}
                            .header img {{
                                max-width: 110px;
                                margin-bottom: 20px;
                                border-radius: 12px;
                                background: white;
                                padding: 10px;
                            }}
                            .header h1 {{
                                color: #ffffff;
                                font-size: 30px;
                                font-weight: 700;
                                margin: 0;
                            }}
                            .header-subtitle {{
                                color: #ffffff;
                                font-size: 16px;
                                margin-top: 10px;
                                opacity: 0.95;
                            }}
                            .content {{
                                padding: 40px 30px;
                                color: #333333;
                            }}
                            .greeting {{
                                font-size: 26px;
                                font-weight: 700;
                                color: #f5576c;
                                margin-bottom: 20px;
                            }}
                            .text {{
                                font-size: 16px;
                                line-height: 1.8;
                                color: #555555;
                                margin-bottom: 15px;
                            }}
                            .journey-card {{
                                background: linear-gradient(135deg, #fff0f5 0%, #ffe8f0 100%);
                                border-radius: 12px;
                                padding: 30px;
                                margin: 30px 0;
                                text-align: center;
                            }}
                            .journey-emoji {{
                                font-size: 48px;
                                margin-bottom: 15px;
                            }}
                            .journey-title {{
                                font-size: 22px;
                                font-weight: 700;
                                color: #f5576c;
                                margin-bottom: 10px;
                            }}
                            .journey-text {{
                                font-size: 15px;
                                color: #666;
                                line-height: 1.6;
                            }}
                            .learning-paths {{
                                margin: 30px 0;
                            }}
                            .path-item {{
                                display: flex;
                                align-items: flex-start;
                                padding: 20px;
                                margin-bottom: 15px;
                                background: #ffffff;
                                border-radius: 12px;
                                box-shadow: 0 4px 12px rgba(245, 87, 108, 0.1);
                                border-left: 5px solid #f5576c;
                            }}
                            .path-number {{
                                width: 50px;
                                height: 50px;
                                background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
                                border-radius: 50%;
                                display: flex;
                                align-items: center;
                                justify-content: center;
                                color: white;
                                font-size: 22px;
                                font-weight: 700;
                                margin-right: 20px;
                                flex-shrink: 0;
                            }}
                            .path-content {{
                                flex: 1;
                            }}
                            .path-title {{
                                font-size: 18px;
                                font-weight: 700;
                                color: #333;
                                margin-bottom: 8px;
                            }}
                            .path-desc {{
                                font-size: 15px;
                                color: #666;
                                line-height: 1.5;
                            }}
                            .button-container {{
                                text-align: center;
                                margin: 35px 0;
                            }}
                            .button {{
                                display: inline-block;
                                background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
                                color: #ffffff;
                                padding: 18px 45px;
                                text-decoration: none;
                                border-radius: 30px;
                                font-size: 16px;
                                font-weight: 600;
                                box-shadow: 0 8px 20px rgba(245, 87, 108, 0.4);
                                transition: transform 0.3s ease;
                            }}
                            .motivation-box {{
                                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                                color: white;
                                border-radius: 12px;
                                padding: 25px;
                                margin: 25px 0;
                                text-align: center;
                            }}
                            .motivation-box p {{
                                font-size: 17px;
                                line-height: 1.6;
                                margin: 0;
                                font-weight: 500;
                            }}
                            .motivation-box strong {{
                                font-size: 20px;
                                display: block;
                                margin-bottom: 10px;
                            }}
                            .footer {{
                                background: #f8f9fa;
                                padding: 30px;
                                text-align: center;
                                color: #888888;
                                font-size: 13px;
                                line-height: 1.6;
                            }}
                            .footer-links {{
                                margin-top: 15px;
                            }}
                            .footer-link {{
                                color: #f5576c;
                                text-decoration: none;
                                margin: 0 10px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <div class='header'>
                                <div class='learner-badge'>🚀 Learner Journey</div>
                                <img src='https://media.licdn.com/dms/image/v2/D4D0BAQE_Sg1d16i2Eg/company-logo_200_200/company-logo_200_200/0/1714025968860/thutonet_logo?e=1749081600&v=beta&t=Ksu1go1Z71gXBrcRTRtpC-l5mPCYH64GGnlV49mlQHY' alt='ThutoNet Logo' />
                                <h1>Your Learning Adventure Begins!</h1>
                                <p class='header-subtitle'>Get ready to discover, grow, and achieve your goals</p>
                            </div>
                            <div class='content'>
                                <div class='greeting'>Welcome {dto.FirstName} {dto.LastName}! 🎉</div>
                                <p class='text'>We're absolutely thrilled to have you join the ThutoNet learning community! You've just taken the first step towards unlocking your potential and achieving your dreams.</p>
                                
                                <div class='journey-card'>
                                    <div class='journey-emoji'>🌱</div>
                                    <div class='journey-title'>Every Expert Was Once a Beginner</div>
                                    <p class='journey-text'>Your learning journey is unique, and we're here to support you every step of the way. Whether you're exploring new skills or deepening existing knowledge, ThutoNet is your partner in growth.</p>
                                </div>
                    
                                <div class='learning-paths'>
                                    <div class='path-item'>
                                        <div class='path-number'>1</div>
                                        <div class='path-content'>
                                            <div class='path-title'>Explore Your Interests</div>
                                            <div class='path-desc'>Browse our diverse catalog of courses and find what sparks your curiosity</div>
                                        </div>
                                    </div>
                                    <div class='path-item'>
                                        <div class='path-number'>2</div>
                                        <div class='path-content'>
                                            <div class='path-title'>Learn at Your Pace</div>
                                            <div class='path-desc'>Study when it suits you, with flexible schedules and lifetime access</div>
                                        </div>
                                    </div>
                                    <div class='path-item'>
                                        <div class='path-number'>3</div>
                                        <div class='path-content'>
                                            <div class='path-title'>Apply Your Knowledge</div>
                                            <div class='path-desc'>Complete projects, assignments, and real-world challenges</div>
                                        </div>
                                    </div>
                                    <div class='path-item'>
                                        <div class='path-number'>4</div>
                                        <div class='path-content'>
                                            <div class='path-title'>Achieve & Celebrate</div>
                                            <div class='path-desc'>Earn certificates and showcase your accomplishments</div>
                                        </div>
                                    </div>
                                </div>
                    
                                <p class='text'>To begin your learning journey, please verify your email address:</p>
                                
                                <div class='button-container'>
                                    <a href='{dto.CallBackUrl}' class='button'>Start Learning Now</a>
                                </div>
                    
                                <div class='motivation-box'>
                                    <strong>💪 You've Got This!</strong>
                                    <p>Remember: Progress, not perfection. Every lesson completed is a step forward. We're here to support you all the way!</p>
                                </div>
                                
                                <p class='text' style='font-size: 14px; color: #888;'>Questions? Our support team is always ready to help you succeed!</p>
                            </div>
                            <div class='footer'>
                                <p><strong>ThutoNet Learners</strong></p>
                                <p>Dream. Learn. Achieve. Repeat.</p>
                                <div class='footer-links'>
                                    <a href='#' class='footer-link'>Course Catalog</a> |
                                    <a href='#' class='footer-link'>Learning Tips</a> |
                                    <a href='#' class='footer-link'>Community</a>
                                </div>
                                <p style='margin-top: 15px;'>&copy; {DateTime.UtcNow.Year} ThutoNet. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                await _EmailSender.SendEmailAsync(dto.Email, "Confirmation Email", emailBody);

                return true;
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception, "Error occurred while sending teacher email.");

                return false;
            }
        }

        public async Task<bool> PasswordResetAsync(EmailSenderDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _Logger.LogWarning("Email sender Dto is null");
                    return false;
                }

                string emailBody = $@"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            * {{
                                margin: 0;
                                padding: 0;
                                box-sizing: border-box;
                            }}
                            body {{
                                font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
                                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                                margin: 0;
                                padding: 40px 20px;
                                line-height: 1.6;
                            }}
                            .email-wrapper {{
                                max-width: 600px;
                                margin: 0 auto;
                            }}
                            .email-container {{
                                background-color: #ffffff;
                                border-radius: 16px;
                                overflow: hidden;
                                box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
                            }}
                            .header {{
                                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                                padding: 40px 30px;
                                text-align: center;
                            }}
                            .header img {{
                                max-width: 120px;
                                height: auto;
                                background: white;
                                padding: 15px;
                                border-radius: 12px;
                                box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
                            }}
                            .content {{
                                padding: 40px 30px;
                                color: #333333;
                            }}
                            .content h2 {{
                                color: #1a202c;
                                font-size: 28px;
                                margin-bottom: 20px;
                                font-weight: 700;
                            }}
                            .content p {{
                                font-size: 16px;
                                color: #4a5568;
                                margin-bottom: 15px;
                                line-height: 1.7;
                            }}
                            .greeting {{
                                font-size: 18px;
                                color: #2d3748;
                                font-weight: 600;
                                margin-bottom: 20px;
                            }}
                            .button-container {{
                                text-align: center;
                                margin: 35px 0;
                            }}
                            .button {{
                                display: inline-block;
                                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                                color: #ffffff;
                                padding: 16px 40px;
                                text-decoration: none;
                                border-radius: 8px;
                                font-size: 16px;
                                font-weight: 600;
                                box-shadow: 0 4px 15px rgba(102, 126, 234, 0.4);
                                transition: all 0.3s ease;
                            }}
                            .button:hover {{
                                transform: translateY(-2px);
                                box-shadow: 0 6px 20px rgba(102, 126, 234, 0.5);
                            }}
                            .info-box {{
                                background-color: #f7fafc;
                                border-left: 4px solid #667eea;
                                padding: 15px 20px;
                                margin: 25px 0;
                                border-radius: 4px;
                            }}
                            .info-box p {{
                                margin: 0;
                                font-size: 14px;
                                color: #4a5568;
                            }}
                            .divider {{
                                height: 1px;
                                background: linear-gradient(to right, transparent, #e2e8f0, transparent);
                                margin: 30px 0;
                            }}
                            .footer {{
                                background-color: #f7fafc;
                                padding: 30px;
                                text-align: center;
                                border-top: 1px solid #e2e8f0;
                            }}
                            .footer p {{
                                font-size: 13px;
                                color: #718096;
                                margin-bottom: 8px;
                            }}
                            .social-links {{
                                margin-top: 20px;
                            }}
                            .social-links a {{
                                display: inline-block;
                                margin: 0 10px;
                                color: #667eea;
                                text-decoration: none;
                                font-size: 14px;
                            }}
                            .security-notice {{
                                background-color: #fff5f5;
                                border: 1px solid #feb2b2;
                                border-radius: 8px;
                                padding: 15px;
                                margin-top: 20px;
                            }}
                            .security-notice p {{
                                color: #c53030;
                                font-size: 14px;
                                margin: 0;
                            }}
                            @media only screen and (max-width: 600px) {{
                                body {{
                                    padding: 20px 10px;
                                }}
                                .content {{
                                    padding: 30px 20px;
                                }}
                                .header {{
                                    padding: 30px 20px;
                                }}
                                .content h2 {{
                                    font-size: 24px;
                                }}
                                .button {{
                                    padding: 14px 30px;
                                    font-size: 15px;
                                }}
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='email-wrapper'>
                            <div class='email-container'>
                                <!-- Header -->
                                <div class='header'>
                                    <img src='https://media.licdn.com/dms/image/v2/D4D0BAQE_Sg1d16i2Eg/company-logo_200_200/company-logo_200_200/0/1714025968860/thutonet_logo?e=1749081600&v=beta&t=Ksu1go1Z71gXBrcRTRtpC-l5mPCYH64GGnlV49mlQHY' alt='ThutoNet Logo' />
                                </div>
                                
                                <!-- Content -->
                                <div class='content'>
                                    <h2>🔐 Password Reset Request</h2>
                                    
                                    <p class='greeting'>Hello {dto.FirstName} {dto.LastName},</p>
                                    
                                    <p>We received a request to reset the password for your ThutoNet account. No worries - it happens to the best of us!</p>
                                    
                                    <p>Click the button below to create a new password. This link will expire in 30 minutes for security reasons.</p>
                                    
                                    <!-- Button -->
                                    <div class='button-container'>
                                        <a href='{dto.CallBackUrl}' class='button'>Reset My Password</a>
                                    </div>
                                    
                                    <!-- Info Box -->
                                    <div class='info-box'>
                                        <p><strong>📧 Your email:</strong> {dto.Email}</p>
                                    </div>
                                    
                                    <div class='divider'></div>
                                    
                                    <!-- Security Notice -->
                                    <div class='security-notice'>
                                        <p><strong>⚠️ Security Alert:</strong> If you didn't request this password reset, please ignore this email. Your password will remain unchanged, and your account is secure.</p>
                                    </div>
                                    
                                    <p style='margin-top: 30px;'>Need help? Our support team is always ready to assist you.</p>
                                    
                                    <p style='margin-top: 20px;'>
                                        Best regards,<br>
                                        <strong>The ThutoNet Team</strong>
                                    </p>
                                </div>
                                
                                <!-- Footer -->
                                <div class='footer'>
                                    <p><strong>ThutoNet</strong> - Empowering Education</p>
                                    <p>&copy; {DateTime.Now.Year} ThutoNet. All rights reserved.</p>
                                    
                                    <div class='divider' style='margin: 20px 40px;'></div>
                                    
                                    <p>This is an automated message, please do not reply to this email.</p>
                                    <p>If you have questions, contact us at <a href='mailto:support@thutonet.com' style='color: #667eea; text-decoration: none;'>support@thutonet.com</a></p>
                                    
                                    <div class='social-links'>
                                        <a href='#'>Privacy Policy</a> • 
                                        <a href='#'>Terms of Service</a> • 
                                        <a href='#'>Contact Us</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </body>
                    </html>
                    ";

                await _EmailSender.SendEmailAsync(dto.Email, "Password reset", emailBody);

                return true;

            }
            catch (Exception exception)
            {
                _Logger.LogError(exception.Message, "An Error occured while sending email");

                return false;
            }
        }

        public async Task<bool> StudentEmailSender(EmailSenderDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _Logger.LogWarning("EmailSenderDto is null.");

                    return false;
                }

                string emailBody = $@"
                <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            * {{
                                margin: 0;
                                padding: 0;
                                box-sizing: border-box;
                            }}
                            body {{
                                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                                padding: 40px 20px;
                            }}
                            .email-container {{
                                max-width: 600px;
                                margin: 0 auto;
                                background: #ffffff;
                                border-radius: 16px;
                                overflow: hidden;
                                box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
                            }}
                            .header {{
                                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                                padding: 40px 30px;
                                text-align: center;
                            }}
                            .header img {{
                                max-width: 120px;
                                margin-bottom: 20px;
                                border-radius: 12px;
                                background: white;
                                padding: 10px;
                            }}
                            .header h1 {{
                                color: #ffffff;
                                font-size: 28px;
                                font-weight: 700;
                                margin: 0;
                            }}
                            .content {{
                                padding: 40px 30px;
                                color: #333333;
                            }}
                            .greeting {{
                                font-size: 24px;
                                font-weight: 600;
                                color: #667eea;
                                margin-bottom: 20px;
                            }}
                            .text {{
                                font-size: 16px;
                                line-height: 1.8;
                                color: #555555;
                                margin-bottom: 15px;
                            }}
                            .features {{
                                background: #f8f9ff;
                                border-radius: 12px;
                                padding: 25px;
                                margin: 30px 0;
                            }}
                            .feature-item {{
                                display: flex;
                                align-items: center;
                                margin-bottom: 15px;
                            }}
                            .feature-icon {{
                                width: 40px;
                                height: 40px;
                                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                                border-radius: 50%;
                                display: flex;
                                align-items: center;
                                justify-content: center;
                                color: white;
                                font-weight: bold;
                                margin-right: 15px;
                                flex-shrink: 0;
                            }}
                            .feature-text {{
                                font-size: 15px;
                                color: #333;
                            }}
                            .button-container {{
                                text-align: center;
                                margin: 35px 0;
                            }}
                            .button {{
                                display: inline-block;
                                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                                color: #ffffff;
                                padding: 16px 40px;
                                text-decoration: none;
                                border-radius: 30px;
                                font-size: 16px;
                                font-weight: 600;
                                box-shadow: 0 8px 20px rgba(102, 126, 234, 0.4);
                                transition: transform 0.3s ease;
                            }}
                            .button:hover {{
                                transform: translateY(-2px);
                            }}
                            .divider {{
                                height: 1px;
                                background: linear-gradient(to right, transparent, #e0e0e0, transparent);
                                margin: 30px 0;
                            }}
                            .footer {{
                                background: #f8f9fa;
                                padding: 30px;
                                text-align: center;
                                color: #888888;
                                font-size: 13px;
                                line-height: 1.6;
                            }}
                            .footer-links {{
                                margin-top: 15px;
                            }}
                            .footer-link {{
                                color: #fc4a1a;
                                text-decoration: none;
                                margin: 0 10px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <div class='header'>
                                <div class='staff-badge'>👔 Staff Member</div>
                                <img src='https://media.licdn.com/dms/image/v2/D4D0BAQE_Sg1d16i2Eg/company-logo_200_200/company-logo_200_200/0/1714025968860/thutonet_logo?e=1749081600&v=beta&t=Ksu1go1Z71gXBrcRTRtpC-l5mPCYH64GGnlV49mlQHY' alt='ThutoNet Logo' />
                                <h1>Welcome to the Team!</h1>
                            </div>
                            <div class='content'>
                                <div class='greeting'>Hello {dto.FirstName} {dto.LastName}!</div>
                                <p class='text'>We're excited to welcome you as a staff member at ThutoNet! You're now part of a dedicated team working to make education accessible and impactful.</p>
                                
                                <div class='welcome-card'>
                                    <div class='welcome-title'>Your Role Matters</div>
                                    <p class='welcome-text'>As a staff member, you play a crucial role in supporting our educational mission. Your contributions help create an environment where students and teachers can thrive.</p>
                                </div>
                    
                                <div class='responsibilities'>
                                    <div class='resp-item'>
                                        <div class='resp-icon'>🎯</div>
                                        <div class='resp-text'><strong>Operational Support</strong> - Assist with platform operations and user support</div>
                                    </div>
                                    <div class='resp-item'>
                                        <div class='resp-icon'>📋</div>
                                        <div class='resp-text'><strong>Content Management</strong> - Help organize and maintain educational resources</div>
                                    </div>
                                    <div class='resp-item'>
                                        <div class='resp-icon'>🤝</div>
                                        <div class='resp-text'><strong>User Assistance</strong> - Provide guidance to students and teachers</div>
                                    </div>
                                    <div class='resp-item'>
                                        <div class='resp-icon'>📈</div>
                                        <div class='resp-text'><strong>Quality Assurance</strong> - Ensure smooth platform functionality</div>
                                    </div>
                                    <div class='resp-item'>
                                        <div class='resp-icon'>💡</div>
                                        <div class='resp-text'><strong>Innovation</strong> - Contribute ideas for platform improvements</div>
                                    </div>
                                </div>
                    
                                <p class='text'>To activate your staff account and access your workspace, please verify your email address:</p>
                                
                                <div class='button-container'>
                                    <a href='{dto.CallBackUrl}' class='button'>Verify Staff Account</a>
                                </div>
                    
                                <div class='team-welcome'>
                                    <p><strong>🌟 You're part of something special!</strong><br>Our team is here to support you. Don't hesitate to reach out if you need anything.</p>
                                </div>
                                
                                <p class='text' style='font-size: 14px; color: #888;'>If you have questions about your role or need assistance, our HR team is available to help.</p>
                            </div>
                            <div class='footer'>
                                <p><strong>ThutoNet Staff</strong></p>
                                <p>Together We Achieve More</p>
                                <div class='footer-links'>
                                    <a href='#' class='footer-link'>Staff Portal</a> |
                                    <a href='#' class='footer-link'>Resources</a> |
                                    <a href='#' class='footer-link'>HR Support</a>
                                </div>
                                <p style='margin-top: 15px;'>&copy; {DateTime.UtcNow.Year} ThutoNet. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                await _EmailSender.SendEmailAsync(dto.Email, "Confirmation Email", emailBody);

                return true;
            }

            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while sending student email.");
                return false;
            }
        }

        public async Task<bool> StuffMemberEmailSender(EmailSenderDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _Logger.LogWarning("EmailSenderDto is null.");
                    return false;
                }

                string emailBody = $@"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            * {{
                                margin: 0;
                                padding: 0;
                                box-sizing: border-box;
                            }}
                            body {{
                                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                background: linear-gradient(135deg, #fc4a1a 0%, #f7b733 100%);
                                padding: 40px 20px;
                            }}
                            .email-container {{
                                max-width: 600px;
                                margin: 0 auto;
                                background: #ffffff;
                                border-radius: 16px;
                                overflow: hidden;
                                box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
                            }}
                            .header {{
                                background: linear-gradient(135deg, #fc4a1a 0%, #f7b733 100%);
                                padding: 50px 30px;
                                text-align: center;
                            }}
                            .staff-badge {{
                                background: #ffffff;
                                color: #fc4a1a;
                                padding: 8px 20px;
                                border-radius: 20px;
                                font-size: 12px;
                                font-weight: 700;
                                text-transform: uppercase;
                                letter-spacing: 1px;
                                display: inline-block;
                                margin-bottom: 20px;
                            }}
                            .header img {{
                                max-width: 110px;
                                margin-bottom: 20px;
                                border-radius: 12px;
                                background: white;
                                padding: 10px;
                            }}
                            .header h1 {{
                                color: #ffffff;
                                font-size: 30px;
                                font-weight: 700;
                                margin: 0;
                            }}
                            .content {{
                                padding: 40px 30px;
                                color: #333333;
                            }}
                            .greeting {{
                                font-size: 26px;
                                font-weight: 700;
                                color: #fc4a1a;
                                margin-bottom: 20px;
                            }}
                            .text {{
                                font-size: 16px;
                                line-height: 1.8;
                                color: #555555;
                                margin-bottom: 15px;
                            }}
                            .welcome-card {{
                                background: linear-gradient(135deg, #fff5f0 0%, #ffe8d6 100%);
                                border-radius: 12px;
                                padding: 30px;
                                margin: 30px 0;
                                text-align: center;
                            }}
                            .welcome-title {{
                                font-size: 22px;
                                font-weight: 700;
                                color: #fc4a1a;
                                margin-bottom: 15px;
                            }}
                            .welcome-text {{
                                font-size: 15px;
                                color: #666;
                                line-height: 1.6;
                            }}
                            .responsibilities {{
                                margin: 30px 0;
                            }}
                            .resp-item {{
                                display: flex;
                                align-items: center;
                                padding: 15px;
                                margin-bottom: 12px;
                                background: #f8f9fa;
                                border-radius: 8px;
                                border-left: 4px solid #fc4a1a;
                            }}
                            .resp-icon {{
                                width: 40px;
                                height: 40px;
                                background: linear-gradient(135deg, #fc4a1a 0%, #f7b733 100%);
                                border-radius: 8px;
                                display: flex;
                                align-items: center;
                                justify-content: center;
                                color: white;
                                font-size: 18px;
                                margin-right: 15px;
                                flex-shrink: 0;
                            }}
                            .resp-text {{
                                font-size: 15px;
                                color: #333;
                            }}
                            .button-container {{
                                text-align: center;
                                margin: 35px 0;
                            }}
                            .button {{
                                display: inline-block;
                                background: linear-gradient(135deg, #fc4a1a 0%, #f7b733 100%);
                                color: #ffffff;
                                padding: 18px 45px;
                                text-decoration: none;
                                border-radius: 30px;
                                font-size: 16px;
                                font-weight: 600;
                                box-shadow: 0 8px 20px rgba(252, 74, 26, 0.4);
                                transition: transform 0.3s ease;
                            }}
                            .team-welcome {{
                                background: #f8f9fa;
                                border-radius: 12px;
                                padding: 25px;
                                margin: 25px 0;
                                text-align: center;
                            }}
                            .team-welcome p {{
                                font-size: 15px;
                                color: #555;
                                margin: 0;
                            }}
                            .footer {{
                                background: #f8f9fa;
                                padding: 30px;
                                text-align: center;
                                color: #888888;
                                font-size: 13px;
                                line-height: 1.6;
                            }}
                            .footer-links {{
                                margin-top: 15px;
                            }}
                            .footer-link {{
                                color: #fc4a1a;
                                text-decoration: none;
                                margin: 0 10px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <div class='header'>
                                <div class='staff-badge'>👔 Staff Member</div>
                                <img src='https://media.licdn.com/dms/image/v2/D4D0BAQE_Sg1d16i2Eg/company-logo_200_200/company-logo_200_200/0/1714025968860/thutonet_logo?e=1749081600&v=beta&t=Ksu1go1Z71gXBrcRTRtpC-l5mPCYH64GGnlV49mlQHY' alt='ThutoNet Logo' />
                                <h1>Welcome to the Team!</h1>
                            </div>
                            <div class='content'>
                                <div class='greeting'>Hello {dto.FirstName} {dto.LastName}!</div>
                                <p class='text'>We're excited to welcome you as a staff member at ThutoNet! You're now part of a dedicated team working to make education accessible and impactful.</p>
                                
                                <div class='welcome-card'>
                                    <div class='welcome-title'>Your Role Matters</div>
                                    <p class='welcome-text'>As a staff member, you play a crucial role in supporting our educational mission. Your contributions help create an environment where students and teachers can thrive.</p>
                                </div>
                    
                                <div class='responsibilities'>
                                    <div class='resp-item'>
                                        <div class='resp-icon'>🎯</div>
                                        <div class='resp-text'><strong>Operational Support</strong> - Assist with platform operations and user support</div>
                                    </div>
                                    <div class='resp-item'>
                                        <div class='resp-icon'>📋</div>
                                        <div class='resp-text'><strong>Content Management</strong> - Help organize and maintain educational resources</div>
                                    </div>
                                    <div class='resp-item'>
                                        <div class='resp-icon'>🤝</div>
                                        <div class='resp-text'><strong>User Assistance</strong> - Provide guidance to students and teachers</div>
                                    </div>
                                    <div class='resp-item'>
                                        <div class='resp-icon'>📈</div>
                                        <div class='resp-text'><strong>Quality Assurance</strong> - Ensure smooth platform functionality</div>
                                    </div>
                                    <div class='resp-item'>
                                        <div class='resp-icon'>💡</div>
                                        <div class='resp-text'><strong>Innovation</strong> - Contribute ideas for platform improvements</div>
                                    </div>
                                </div>
                    
                                <p class='text'>To activate your staff account and access your workspace, please verify your email address:</p>
                                
                                <div class='button-container'>
                                    <a href='{dto.CallBackUrl}' class='button'>Verify Staff Account</a>
                                </div>
                    
                                <div class='team-welcome'>
                                    <p><strong>🌟 You're part of something special!</strong><br>Our team is here to support you. Don't hesitate to reach out if you need anything.</p>
                                </div>
                                
                                <p class='text' style='font-size: 14px; color: #888;'>If you have questions about your role or need assistance, our HR team is available to help.</p>
                            </div>
                            <div class='footer'>
                                <p><strong>ThutoNet Staff</strong></p>
                                <p>Together We Achieve More</p>
                                <div class='footer-links'>
                                    <a href='#' class='footer-link'>Staff Portal</a> |
                                    <a href='#' class='footer-link'>Resources</a> |
                                    <a href='#' class='footer-link'>HR Support</a>
                                </div>
                                <p style='margin-top: 15px;'>&copy; {DateTime.UtcNow.Year} ThutoNet. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                await _EmailSender.SendEmailAsync(dto.Email!, "Confirmation Email", emailBody);

                return true;
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception, "Error occurred while sending staff member email.");
                return false;
            }
        }

        public async Task<bool> TeacherEmailSender(EmailSenderDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _Logger.LogWarning("EmailSenderDto is null.");
                    return false;
                }

                string emailBody = $@"
                    <!DOCTYPE html>
                       <html lang='en'>
                       <head>
                           <meta charset='UTF-8'>
                           <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                           <style>
                               * {{
                                   margin: 0;
                                   padding: 0;
                                   box-sizing: border-box;
                               }}
                               body {{
                                   font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                   background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
                                   padding: 40px 20px;
                               }}
                               .email-container {{
                                   max-width: 600px;
                                   margin: 0 auto;
                                   background: #ffffff;
                                   border-radius: 16px;
                                   overflow: hidden;
                                   box-shadow: 0 20px 60px rgba(0, 0, 0, 0.3);
                               }}
                               .header {{
                                   background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
                                   padding: 50px 30px;
                                   text-align: center;
                               }}
                               .teacher-badge {{
                                   background: #ffffff;
                                   color: #11998e;
                                   padding: 8px 20px;
                                   border-radius: 20px;
                                   font-size: 12px;
                                   font-weight: 700;
                                   text-transform: uppercase;
                                   letter-spacing: 1px;
                                   display: inline-block;
                                   margin-bottom: 20px;
                               }}
                               .header img {{
                                   max-width: 110px;
                                   margin-bottom: 20px;
                                   border-radius: 12px;
                                   background: white;
                                   padding: 10px;
                               }}
                               .header h1 {{
                                   color: #ffffff;
                                   font-size: 30px;
                                   font-weight: 700;
                                   margin: 0;
                               }}
                               .content {{
                                   padding: 40px 30px;
                                   color: #333333;
                               }}
                               .greeting {{
                                   font-size: 26px;
                                   font-weight: 700;
                                   color: #11998e;
                                   margin-bottom: 20px;
                               }}
                               .text {{
                                   font-size: 16px;
                                   line-height: 1.8;
                                   color: #555555;
                                   margin-bottom: 15px;
                               }}
                               .teacher-tools {{
                                   background: linear-gradient(135deg, #f0fff4 0%, #e6fffa 100%);
                                   border-radius: 12px;
                                   padding: 30px;
                                   margin: 30px 0;
                               }}
                               .tools-title {{
                                   font-size: 20px;
                                   font-weight: 700;
                                   color: #11998e;
                                   margin-bottom: 20px;
                                   text-align: center;
                               }}
                               .tool-item {{
                                   display: flex;
                                   align-items: flex-start;
                                   margin-bottom: 18px;
                                   padding: 15px;
                                   background: white;
                                   border-radius: 8px;
                                   box-shadow: 0 2px 8px rgba(17, 153, 142, 0.1);
                               }}
                               .tool-icon {{
                                   width: 45px;
                                   height: 45px;
                                   background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
                                   border-radius: 10px;
                                   display: flex;
                                   align-items: center;
                                   justify-content: center;
                                   color: white;
                                   font-size: 20px;
                                   margin-right: 15px;
                                   flex-shrink: 0;
                               }}
                               .tool-content {{
                                   flex: 1;
                               }}
                               .tool-title {{
                                   font-weight: 600;
                                   color: #11998e;
                                   margin-bottom: 5px;
                               }}
                               .tool-desc {{
                                   font-size: 14px;
                                   color: #666;
                               }}
                               .button-container {{
                                   text-align: center;
                                   margin: 35px 0;
                               }}
                               .button {{
                                   display: inline-block;
                                   background: linear-gradient(135deg, #11998e 0%, #38ef7d 100%);
                                   color: #ffffff;
                                   padding: 18px 45px;
                                   text-decoration: none;
                                   border-radius: 30px;
                                   font-size: 16px;
                                   font-weight: 600;
                                   box-shadow: 0 8px 20px rgba(17, 153, 142, 0.4);
                                   transition: transform 0.3s ease;
                               }}
                               .quote {{
                                   background: #f8f9fa;
                                   border-left: 4px solid #11998e;
                                   padding: 20px;
                                   margin: 25px 0;
                                   font-style: italic;
                                   color: #555;
                               }}
                               .footer {{
                                   background: #f8f9fa;
                                   padding: 30px;
                                   text-align: center;
                                   color: #888888;
                                   font-size: 13px;
                                   line-height: 1.6;
                               }}
                               .footer-links {{
                                   margin-top: 15px;
                               }}
                               .footer-link {{
                                   color: #11998e;
                                   text-decoration: none;
                                   margin: 0 10px;
                               }}
                           </style>
                       </head>
                       <body>
                           <div class='email-container'>
                               <div class='header'>
                                   <div class='teacher-badge'>🎓 Educator Account</div>
                                   <img src='https://media.licdn.com/dms/image/v2/D4D0BAQE_Sg1d16i2Eg/company-logo_200_200/company-logo_200_200/0/1714025968860/thutonet_logo?e=1749081600&v=beta&t=Ksu1go1Z71gXBrcRTRtpC-l5mPCYH64GGnlV49mlQHY' alt='ThutoNet Logo' />
                                   <h1>Welcome to Our Teaching Team!</h1>
                               </div>
                               <div class='content'>
                                   <div class='greeting'>Dear {dto.FirstName} {dto.LastName},</div>
                                   <p class='text'>We're honored to have you join ThutoNet as an educator! Your expertise and passion for teaching will make a significant impact on our students' learning journey.</p>
                                   
                                   <div class='quote'>
                                       ""Education is the most powerful weapon which you can use to change the world."" - Nelson Mandela
                                   </div>
                       
                                   <div class='teacher-tools'>
                                       <div class='tools-title'>Your Teaching Toolkit</div>
                                       <div class='tool-item'>
                                           <div class='tool-icon'>📚</div>
                                           <div class='tool-content'>
                                               <div class='tool-title'>Course Management</div>
                                               <div class='tool-desc'>Create, organize, and publish engaging course content</div>
                                           </div>
                                       </div>
                                       <div class='tool-item'>
                                           <div class='tool-icon'>✏️</div>
                                           <div class='tool-content'>
                                               <div class='tool-title'>Assignment & Grading</div>
                                               <div class='tool-desc'>Design assessments and provide meaningful feedback</div>
                                           </div>
                                       </div>
                                       <div class='tool-item'>
                                           <div class='tool-icon'>📊</div>
                                           <div class='tool-content'>
                                               <div class='tool-title'>Student Analytics</div>
                                               <div class='tool-desc'>Track progress and identify learning opportunities</div>
                                           </div>
                                       </div>
                                       <div class='tool-item'>
                                           <div class='tool-icon'>💬</div>
                                           <div class='tool-content'>
                                               <div class='tool-title'>Communication Hub</div>
                                               <div class='tool-desc'>Connect with students through discussions and messaging</div>
                                           </div>
                                       </div>
                                       <div class='tool-item'>
                                           <div class='tool-icon'>🎥</div>
                                           <div class='tool-content'>
                                               <div class='tool-title'>Multimedia Resources</div>
                                               <div class='tool-desc'>Upload videos, documents, and interactive materials</div>
                                           </div>
                                       </div>
                                   </div>
                       
                                   <p class='text'>To begin your teaching journey with us, please confirm your email address:</p>
                                   
                                   <div class='button-container'>
                                       <a href='{dto.CallBackUrl}' class='button'>Activate Teacher Account</a>
                                   </div>
                                   
                                   <p class='text' style='font-size: 14px; color: #888;'>If you have any questions, our support team is here to help you get started.</p>
                               </div>
                               <div class='footer'>
                                   <p><strong>ThutoNet Educators</strong></p>
                                   <p>Empowering Teachers, Inspiring Students</p>
                                   <div class='footer-links'>
                                       <a href='#' class='footer-link'>Teacher Resources</a> |
                                       <a href='#' class='footer-link'>Training Center</a> |
                                       <a href='#' class='footer-link'>Community Forum</a>
                                   </div>
                                   <p style='margin-top: 15px;'>&copy; {{DateTime.UtcNow.Year}} ThutoNet. All rights reserved.</p>
                               </div>
                           </div>
                       </body>
                       </html>";

                await _EmailSender.SendEmailAsync(dto.Email, "Confirmation Email", emailBody);

                return true;
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception, "Error occurred while sending teacher email.");

                return false;
            }
        }
    }
}

