---
title: Email addins released
category: Announcement
author: jericho
---

There are many scenarios where you need to send notifications during your build process, such as notifying colleagues that a build has passed/failed, notifying stakeholders that a package has been published to nuget, etc. One of the most popular ways to send such notification is, of course, email. That's why we recently released three new addins to send emails.

__[Cake.Email](https://www.nuget.org/packages/Cake.Email)__

The first addin allows you to send emails via your own SMTP server. 

When invoking the `Email.SendEmail` alias, you must specify the name and email address of the sender (this is what the recipient will see in the `from` field when they receive your email). Typically, you would use your name and email address but you could use a generic address such as `notifications@yourcompany.com`), the name and email address of the recipient, the subject and content of the email, a boolean value indicating if the content should be sent as HTML or plain text and finally you must also specify the settings that are necessary to establish a connection to your SMTP server.

The source is available on [GitHub](https://github.com/cake-contrib/Cake.Email).

Here's a code sample that demonstrates how you can send an email from a Cake task:

```csharp
#addin Cake.Email

Task("SendEmail")
    .Does(() =>
{
    try
    {
        var result = Email.Send(
                senderName: "Bob Smith", 
                senderAddress: "bob@example.com",
                recipientName: "Jane Doe",
                recipientAddress: "jane@example.com",
                subject: "This is a test",
                content: "<html><body>This is a test</body></html>",
                sendAsHtml: true,
                settings: new EmailSettings 
                {
                    SmtpHost = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Username = "my_gmail_address@gmail.com",
                    Password = "my_password"
                }
        );

        if (result.Ok)
        {
            Information("Email succcessfully sent");
        }
        else
        {
            Error("Failed to send email: {0}", result.Error);
        }
    }
    catch(Exception ex)
    {
        Error("{0}", ex);
    }
});
```


__[Cake.SendGrid](https://www.nuget.org/packages/Cake.SendGrid/)__

The second addin allows you to send emails via a third-party Email Service Provider (ESP) called [SendGrid](https://sendgrid.com). The parameters you pass to the `SendGrid.SendEmail` alias are very similar to what we described earlier except that it accepts both the HTML and the plain text versions which will be sent in the same "multi-part" email, and also the only setting expected by SendGrid is your API Key.

The source is available on [GitHub](https://github.com/cake-contrib/Cake.SendGrid).

Please note that this addin has a dependency on a library I created which allows using SendGrid's v3 API. The library is called [StrongGrid](https://www.nuget.org/packages/StrongGrid/) and source is available on [GitHub](https://github.com/Jericho/StrongGrid).

Here's a code sample that demonstrates how you can send an email via SendGrid from a Cake task:

```csharp
#addin Cake.SendGrid

var sendGridApiKey = EnvironmentVariable("SENDGRID_API_KEY");

Task("SendEmail")
    .Does(() =>
{
    try
    {
        var result = SendGrid.SendEmail(
                senderName: "Bob Smith", 
                senderAddress: "bob@example.com",
                recipientName: "Jane Doe",
                recipientAddress: "jane@example.com",
                subject: "This is a test",
                htmlContent: "<html><body>This is a test</body></html>",
                textContent: "This is a test",
                settings: new SendGridEmailSettings { ApiKey = sendGridApiKey }
        );

        if (result.Ok)
        {
            Information("Email succcessfully sent");
        }
        else
        {
            Error("Failed to send email: {0}", result.Error);
        }
    }
    catch(Exception ex)
    {
        Error("{0}", ex);
    }
});
```

__[Cake.CakeMail](https://www.nuget.org/packages/Cake.CakeMail/)__

And finally, the third addin allows you to send emails via another ESP called [CakeMail](https://cakemail.com) (the fact that their name contains 'Cake' is totally coincidental!). Once again, the expected parameters are very similar except that they do not allow setting the name of the recipient and they expect a username and password in addition to an API key.

The source is available on [GitHub](https://github.com/cake-contrib/Cake.CakeMail).

Please note that this addin has a dependency on a library I created which allows using CakeMail's API. The library is called [CakeMail.RestClient](https://www.nuget.org/packages/CakeMail.RestClient/) and source is available on [GitHub](https://github.com/Jericho/CakeMail.RestClient).

Here's a code sample that demonstrates how you can send an email via CakeMail from a Cake task:

```csharp
#addin Cake.CakeMail

var apiKey = EnvironmentVariable("CAKEMAIL_API_KEY");
var userName = EnvironmentVariable("CAKEMAIL_USERNAME");
var password = EnvironmentVariable("CAKEMAIL_PASSWORD");

Task("SendEmail")
    .Does(() =>
{
    try
    {
        var result = CakeMail.SendEmail(
                senderName: "Bob Smith", 
                senderAddress: "bob@example.com",
                recipientAddress: "jane@example.com",
                subject: "This is a test",
                htmlContent: "<html><body>This is a test</body></html>",
                textContent: "This is a test",
                settings: new CakeMailEmailSettings
                {
                    ApiKey = apiKey,
                    UserName = userName,
                    Password = password
                }
        );

        if (result.Ok)
        {
            Information("Email succcessfully sent");
        }
        else
        {
            Error("Failed to send email: {0}", result.Error);
        }
    }
    catch(Exception ex)
    {
        Error("{0}", ex);
    }
});
```
