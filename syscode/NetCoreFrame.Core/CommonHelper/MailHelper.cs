using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace NetCoreFrame.Core.ApiDto
{
    /// <summary>
    /// 邮件发送帮助类
    /// </summary>
    public static class MailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="FromMial">发件人邮箱</param>
        /// <param name="ToMial">收件人邮箱(多个收件人地址用";"号隔开)</param>
        /// <param name="AuthorizationCode">发件人授权码</param>
        /// <param name="ReplyTo">对方回复邮件时默认的接收地址（不设置也是可以的）</param>
        /// <param name="CCMial">//邮件的抄送者(多个抄送人用";"号隔开)</param>
        /// <param name="File_Path">附件的地址</param>
        /// <returns></returns>
        public static bool SendMail(string FromMial, string ToMial, string AuthorizationCode, string ReplyTo, string CCMial, string File_Path)
        {
            bool SendSuccess = true;
            try
            {
                //实例化一个发送邮件类。
                MailMessage mailMessage = new MailMessage();

                //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
                mailMessage.Priority = MailPriority.Normal;

                //发件人邮箱地址。
                mailMessage.From = new MailAddress(FromMial);

                //收件人邮箱地址。需要群发就写多个
                //拆分邮箱地址
                List<string> ToMiallist = ToMial.Split(';').ToList();
                for (int i = 0; i < ToMiallist.Count; i++)
                {
                    mailMessage.To.Add(new MailAddress(ToMiallist[i]));  //收件人邮箱地址。
                }

                if (ReplyTo == "" || ReplyTo == null)
                {
                    ReplyTo = FromMial;
                }

                //对方回复邮件时默认的接收地址(不设置也是可以的哟)
                mailMessage.ReplyTo = new MailAddress(ReplyTo);

                if (CCMial != "" && CCMial != null)
                {
                    List<string> CCMiallist = ToMial.Split(';').ToList();
                    for (int i = 0; i < CCMiallist.Count; i++)
                    {
                        //邮件的抄送者，支持群发
                        mailMessage.CC.Add(new MailAddress(CCMial));
                    }
                }
                //如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
                mailMessage.SubjectEncoding = Encoding.GetEncoding(936);

                //邮件正文是否是HTML格式
                mailMessage.IsBodyHtml = false;

                //邮件标题。
                mailMessage.Subject = "发送邮件测试";
                //邮件内容。
                mailMessage.Body = "测试群发邮件,以及附件邮件！.....";

                //设置邮件的附件，将在客户端选择的附件先上传到服务器保存一个，然后加入到mail中  
                if (File_Path != "" && File_Path != null)
                {
                    //将附件添加到邮件
                    mailMessage.Attachments.Add(new Attachment(File_Path));
                    //获取或设置此电子邮件的发送通知。
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                }

                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();

                #region 设置邮件服务器地址

                //在这里我使用的是163邮箱，所以是smtp.163.com，如果你使用的是qq邮箱，那么就是smtp.qq.com。
                // client.Host = "smtp.163.com";
                if (FromMial.Length != 0)
                {
                    //根据发件人的邮件地址判断发件服务器地址   默认端口一般是25
                    string[] addressor = FromMial.Trim().Split(new Char[] { '@', '.' });
                    switch (addressor[1])
                    {
                        case "163":
                            client.Host = "smtp.163.com";
                            break;
                        case "126":
                            client.Host = "smtp.126.com";
                            break;
                        case "qq":
                            client.Host = "smtp.qq.com";
                            break;
                        case "gmail":
                            client.Host = "smtp.gmail.com";
                            break;
                        case "hotmail":
                            client.Host = "smtp.live.com";//outlook邮箱
                            //client.Port = 587;
                            break;
                        case "foxmail":
                            client.Host = "smtp.foxmail.com";
                            break;
                        case "sina":
                            client.Host = "smtp.sina.com.cn";
                            break;
                        default:
                            client.Host = "smtp.exmail.qq.com";//qq企业邮箱
                            break;
                    }
                }
                #endregion

                //使用安全加密连接。
                client.EnableSsl = true;
                //不和请求一块发送。
                client.UseDefaultCredentials = false;

                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new NetworkCredential(FromMial, AuthorizationCode);

                //如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //发送
                client.Send(mailMessage);
               
            }
            catch (Exception ex)
            {
                SendSuccess = false;
               
            }
            return SendSuccess;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailTo"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailContent"></param>
        /// <returns></returns>
        public static async Task SendEmail(string mailTo, string mailSubject, string mailContent)
        {
            
            // 设置发送方的邮件信息,例如使用网易的smtp
            string smtpServer = "smtp.163.com";
            string mailFrom = "wubiansite@163.com"; //登陆用户名
            string userPassword = "ywk19920212";//授权码

            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient(smtpServer);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            // smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            // smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

            // 发送邮件设置       
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo); // 发送人和收件人
            mailMessage.Subject = mailSubject;//主题
            mailMessage.Body = mailContent;//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Low;//优先级
          
            try
            {
                smtpClient.Send(mailMessage); // 发送邮件
              
            }
            catch (SmtpException ex)
            {
                

            }
        }


        #region 邮件发送
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="sendName">发送者名称</param>
        /// <param name="sendAccountName">发送者账号</param>
        /// <param name="smtpHost">发送者服务器地址：例如：smtp.163.com</param>
        /// <param name="smtpPort">服务器端口号：例如：25</param>
        /// <param name="authenticatePassword">发送者登录邮箱账号的客户端授权码</param>
        /// <param name="receiverAccountNameList">接收者账号</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="sendHtml">文本html(与sendText参数互斥，传此值则 sendText传null)</param>
        /// <param name="sendText">纯文本(与sendHtml参数互斥，传此值则 sendHtml传null)</param>
        /// <param name="accessoryList">邮件的附件</param>
        public static void SendMailMimeKit(string sendName, string sendAccountName, string smtpHost, int smtpPort, string authenticatePassword, List<string> receiverAccountNameList, string mailSubject, string sendHtml, string sendText, List<MimeKit.MimePart> accessoryList = null)
        {
            var message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress(sendName, sendAccountName));
            var mailboxAddressList = new List<MailboxAddress>();
            receiverAccountNameList.ForEach(f =>
            {

                mailboxAddressList.Add(new MailboxAddress(f));
            });
            message.To.AddRange(mailboxAddressList);

            message.Subject = mailSubject;

            var alternative = new MimeKit.Multipart("alternative");
            if (!string.IsNullOrWhiteSpace(sendText))
            {
                alternative.Add(new MimeKit.TextPart("plain")
                {
                    Text = sendText
                });
            }

            if (!string.IsNullOrWhiteSpace(sendHtml))
            {
                alternative.Add(new MimeKit.TextPart("html")
                {
                    Text = sendHtml
                });
            }
            var multipart = new MimeKit.Multipart("mixed");
            multipart.Add(alternative);
            if (accessoryList != null)
            {
                accessoryList?.ForEach(f =>
                {
                    multipart.Add(f);
                });

            }
            message.Body = multipart;
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(smtpHost, smtpPort, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(sendAccountName, authenticatePassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }

        #endregion
    }
}
