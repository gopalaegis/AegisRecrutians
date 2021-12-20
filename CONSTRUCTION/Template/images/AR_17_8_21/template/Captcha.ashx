?<%@ WebHandler Language="C#" Class="Captcha" %>


using System;
using System.Web;

public class Captcha : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        // Create a random code and store it in the Session object.
        context.Session["CaptchaImageText"] = GenerateRandomCode();

        // Create a CAPTCHA image using the text stored in the Session object.
        using (CaptchaImage ci = new CaptchaImage(context.Session["CaptchaImageText"].ToString(), 90, 26, "Arial"))
        {
            // Change the response headers to output a JPEG image.
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";

            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Expires = 0;

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }


    private string GenerateRandomCode()
    {
        Random random = new Random();
        string s = "";
        for (int i = 0; i < 6; i++)
            s = String.Concat(s, random.Next(10).ToString());
        return s;
    }

    public bool IsReusable { get { return false; } }

}