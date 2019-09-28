namespace FutureOfMedia.Infra.Helpers
{
    public class ImageUrlHelper
    {
        //i made this helper to Return the URL of the File instead of the Logical file (Like c:\Dev\YourProject\YourApi\Images\User.jpg)
        public static string ReturnImgUrl(int userId, string imgPath)
        {
            if (imgPath == "" || imgPath == null) return "";
            if(imgPath.EndsWith(".jpg"))
            return "https://localhost:44333/images/User" + userId.ToString() + ".jpg";
            else
                return "https://localhost:44333/images/User" + userId.ToString() + ".png";
        }
    }
}
