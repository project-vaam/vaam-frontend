using System.Text;

namespace TelerikWebAppResponsive
{
    internal class StringContent
    {
        private string v1;
        private Encoding uTF8;
        private string v2;

        public StringContent(string v1, Encoding uTF8, string v2)
        {
            this.v1 = v1;
            this.uTF8 = uTF8;
            this.v2 = v2;
        }
    }
}