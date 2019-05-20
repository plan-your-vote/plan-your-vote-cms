using Xunit;

namespace BackEndTests
{
    public class CandidatesControllerTest
    {
        [Fact]
        public void Equal_Random_Image_Id_Value()
        {
            string image_value_1 = Utility.GetCurrentDateTime;
            string image_value_2 = Utility.GetCurrentDateTime;

            Assert.NotEqual(image_value_1, image_value_2);
        }

        [Fact]
        public void NotNull_Random_Image_Id_Value()
        {
            string image_value_1 = Utility.GetCurrentDateTime;

            Assert.NotNull(image_value_1);
        }
    }
}
