// using NUnit.Framework;

// [TestFixture]
// public class FakeTimeSystemTests
// {
//     [Test]
//     public void UpdateTime_IncreasesCurrentTime()
//     {
//         // Arrange
//         var fakeTimeSystem = new FakeTimeSystem();
//         float deltaTime = 10.0f; // Example deltaTime value.

//         // Act
//         fakeTimeSystem.UpdateTime(deltaTime);

//         // Assert
//         Assert.Greater(fakeTimeSystem.CurrentTime, 0.0f);
//     }

//     [Test]
//     public void FormatCurrentTime_ReturnsFormattedString()
//     {
//         // Arrange
//         var fakeTimeSystem = new FakeTimeSystem();

//         // Act
//         string formattedTime = fakeTimeSystem.FormatCurrentTime();

//         // Assert
//         Assert.IsNotNull(formattedTime);
//         Assert.IsNotEmpty(formattedTime);
//     }

//     // Add more unit tests for other methods and properties as needed.
// }
