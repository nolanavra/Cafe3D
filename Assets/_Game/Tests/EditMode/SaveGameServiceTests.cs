using System.IO;
using Cafe3D.Core.SaveLoad;
using NUnit.Framework;

namespace Cafe3D.Tests.EditMode
{
    public sealed class SaveGameServiceTests
    {
        [Test]
        public void SaveThenLoad_RoundTripsVersionedData()
        {
            var path = Path.GetTempFileName();
            var service = new SaveGameService();
            var data = new SaveGameData { Version = "1.0.0", DayIndex = 2, CurrentCafeId = "LakeEffectCafe" };

            service.Save(path, data);
            var loaded = service.Load(path);

            Assert.AreEqual("1.0.0", loaded.Version);
            Assert.AreEqual(2, loaded.DayIndex);
            File.Delete(path);
        }
    }
}
