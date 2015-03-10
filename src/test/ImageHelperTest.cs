using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Codentia.Common.Helper;
using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
    /// <summary>
    /// Unit Testing framework for ImageHelper
    /// </summary>
    [TestFixture]
    public class ImageHelperTest
    {
        /// <summary>
        /// Scenario: Test the Encoders property
        /// Expected: Not null, correct number of entries
        /// </summary>
        [Test]
        public void _001_Encoders()
        {
            Assert.That(ImageHelper.Encoders, Is.Not.Null);
            Assert.That(ImageHelper.Encoders.Keys.Count, Is.EqualTo(ImageCodecInfo.GetImageEncoders().Length));
        }

        /// <summary>
        /// Scenario: Resize an image using the ResizeImage method
        /// Expected: Image correctly resized
        /// </summary>
        [Test]
        public void _002_ResizeImage_Valid()
        {
            Image i = Image.FromFile("assets/testimage.jpg");
            Bitmap i2 = ImageHelper.ResizeImage(i, 100, 100);

            Assert.That(i2.Width, Is.EqualTo(100));
            Assert.That(i2.Height, Is.EqualTo(100));
        }

        /// <summary>
        /// Scenario: Call the SaveJpeg method to save an image as a Jpeg
        /// Expected: File saved
        /// </summary>
        [Test]
        public void _003_SaveJpeg_Valid()
        {
            // valid quality
            string file = Path.GetTempFileName() + ".jpg";
            Image i = Image.FromFile("assets/testimage.jpg");
            ImageHelper.SaveJpeg(file, i, 50);
            Assert.That(File.Exists(file), Is.True);

            // invalid quality
            file = Path.GetTempFileName() + ".jpg";
            ImageHelper.SaveJpeg(file, i, 101);
            Assert.That(File.Exists(file), Is.True);
        }

        /// <summary>
        /// Scenario: Call the CropImage method
        /// Expected: Image cropped
        /// </summary>
        [Test]
        public void _004_CropImage_Valid()
        {
            Image i = Image.FromFile("assets/testimage.jpg");

            Image i2 = ImageHelper.CropImage(i, 50, 50);
            Assert.That(i2.Width, Is.EqualTo(50));
            Assert.That(i2.Height, Is.EqualTo(50));
        }

        /// <summary>
        /// Scenario: Call the CropImage method
        /// Expected: Image cropped
        /// </summary>
        [Test]
        public void _005_CropImage_WithAlpha()
        {
            Image i = Image.FromFile("assets/testimage_alpha.gif");

            Image i2 = ImageHelper.CropImage(i, 50, 50);
            Assert.That(i2.Width, Is.EqualTo(50));
            Assert.That(i2.Height, Is.EqualTo(50));
        }
    }
}
