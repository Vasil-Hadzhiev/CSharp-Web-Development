namespace SimpleMvc.Common
{
    public static class BreedHelper
    {
        public static string GetImgSource(string breed)
        {
            switch (breed)
            {
                case "Street Transcended":
                    return "../Content/img/street-transcended.jpg";
                case "American Shorthair":
                    return "../Content/img/american-shorthair.jpg";
                case "Munchkin":
                    return "../Content/img/munchkin.jpg";
                case "Siamese":
                    return "../Content/img/siamese.jpg";
                default: return null;
            }
        }
    }
}