using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Arebis.Text;

namespace Arebis.Text.Tests
{
    [TestClass]
    public class UnidecodeTests
    {
        [TestMethod]
        public void ComparingMethodsTest()
        {
            Assert.AreEqual("0o/oo", "0‰".Unidecode(UnidecoderLevel.Ascii));
            Assert.AreEqual("1 Foobaarees Ogrlok AhlenDur Yuliya OEoeSZo/oo (c)<<'AEO EUR", "1 Fóòbàáréès Øgrløk ÅhlénĐur Юлия ŒœŠŽ‰ ©«´ÆÒ €".Unidecode(UnidecoderLevel.Ascii));
            Assert.AreEqual("2 Fóòbàáréès Øgrløk ÅhlénDur Yuliya OEoeSZo/oo ©«´ÆÒ EUR", "2 Fóòbàáréès Øgrløk ÅhlénĐur Юлия ŒœŠŽ‰ ©«´ÆÒ €".Unidecode(UnidecoderLevel.Ansi));
        }

        [TestMethod]
        public void ToAnsiTest()
        {
            Assert.AreEqual("All regular chars", "All regular chars".Unidecode(UnidecoderLevel.Ansi));

            Assert.AreEqual("\r\n\t ©", "\r\n\t ©".Unidecode(UnidecoderLevel.Ansi));

            Assert.AreEqual("U", "𝒰".Unidecode(UnidecoderLevel.Ansi));

            // https://apps.timwhitlock.info/unicode/inspect?s=%F0%9D%90%80%F0%9D%90%83%F0%9D%90%84%F0%9D%90%85%F0%9D%90%86%F0%9D%90%88%F0%9D%90%8B%F0%9D%90%8D%F0%9D%90%8E%F0%9D%90%91%F0%9D%90%93%F0%9D%90%95%F0%9D%90%96
            Assert.AreEqual("ADEFGILNORTVW", "𝐀𝐃𝐄𝐅𝐆𝐈𝐋𝐍𝐎𝐑𝐓𝐕𝐖".Unidecode(UnidecoderLevel.Ansi));

            Assert.AreEqual("Fóòbàáréès", "Fóòbàáréès".Unidecode(UnidecoderLevel.Ansi));
            Assert.AreEqual("Evgeniya", "Евгения".Unidecode(UnidecoderLevel.Ansi));
            Assert.AreEqual("Zhigulina", "Жигулина".Unidecode(UnidecoderLevel.Ansi));
            Assert.AreEqual("Yuliya", "Юлия".Unidecode(UnidecoderLevel.Ansi));
            Assert.AreEqual("Niscáková Ceresnáková", "Niščáková Čerešňáková".Unidecode(UnidecoderLevel.Ansi));
            Assert.AreEqual("Åhlén Duric", "Åhlén Đuric".Unidecode(UnidecoderLevel.Ansi));
            Assert.AreEqual("Bak Maciag Walachowska Lopuszynski", "Bąk Maciąg Wałachowska Łopuszyński".Unidecode(UnidecoderLevel.Ansi));
            Assert.AreEqual("Øgreid Byrløkken", "Øgreid Byrløkken".Unidecode(UnidecoderLevel.Ansi));
            Assert.AreEqual("Adventure's", "Adventure’s".Unidecode(UnidecoderLevel.Ansi));
        }

        [TestMethod]
        public void ToAsciiTest()
        {
            Assert.AreEqual("All regular chars", "All regular chars".Unidecode(UnidecoderLevel.Ascii));

            Assert.AreEqual("\r\n\t (c)", "\r\n\t ©".Unidecode(UnidecoderLevel.Ascii));

            Assert.AreEqual("U", "𝒰".Unidecode(UnidecoderLevel.Ascii));

            // https://apps.timwhitlock.info/unicode/inspect?s=%F0%9D%90%80%F0%9D%90%83%F0%9D%90%84%F0%9D%90%85%F0%9D%90%86%F0%9D%90%88%F0%9D%90%8B%F0%9D%90%8D%F0%9D%90%8E%F0%9D%90%91%F0%9D%90%93%F0%9D%90%95%F0%9D%90%96
            Assert.AreEqual("ADEFGILNORTVW", "𝐀𝐃𝐄𝐅𝐆𝐈𝐋𝐍𝐎𝐑𝐓𝐕𝐖".Unidecode(UnidecoderLevel.Ascii));

            Assert.AreEqual("Foobaarees", "Fóòbàáréès".Unidecode(UnidecoderLevel.Ascii));
            Assert.AreEqual("Evgeniya", "Евгения".Unidecode(UnidecoderLevel.Ascii));
            Assert.AreEqual("Zhigulina", "Жигулина".Unidecode(UnidecoderLevel.Ascii));
            Assert.AreEqual("Yuliya", "Юлия".Unidecode(UnidecoderLevel.Ascii));
            Assert.AreEqual("Niscakova Ceresnakova", "Niščáková Čerešňáková".Unidecode(UnidecoderLevel.Ascii));
            Assert.AreEqual("Ahlen Duric", "Åhlén Đuric".Unidecode(UnidecoderLevel.Ascii));
            Assert.AreEqual("Bak Maciag Walachowska Lopuszynski", "Bąk Maciąg Wałachowska Łopuszyński".Unidecode(UnidecoderLevel.Ascii));
            Assert.AreEqual("Ogreid Byrlokken", "Øgreid Byrløkken".Unidecode(UnidecoderLevel.Ascii));
            Assert.AreEqual("Adventure's", "Adventure’s".Unidecode(UnidecoderLevel.Ascii));
        }
    }
}
