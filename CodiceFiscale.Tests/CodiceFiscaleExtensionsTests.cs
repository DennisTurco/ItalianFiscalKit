using CodiceFiscale.Entities;
using CodiceFiscale.Enums;

namespace CodiceFiscale.Tests;

public class CodiceFiscaleExtensionsTests
{
    [Fact]
    public void GetAge_BirthdayAlreadyPassedThisYear_ShouldReturnCorrectAge()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        DateOnly dob = today.AddYears(-20).AddMonths(-1);
        var data = new CodiceFiscaleData(Gender.Male, dob, "H501");
        Assert.Equal(20, data.GetAge());
    }

    [Fact]
    public void GetAge_BirthdayNotYetThisYear_ShouldReturnAgePreviousYear()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        DateOnly dob = today.AddYears(-20).AddMonths(1);
        var data = new CodiceFiscaleData(Gender.Male, dob, "H501");
        Assert.Equal(19, data.GetAge());
    }

    [Fact]
    public void GetAge_ExactBirthdayToday_ShouldReturnCorrectAge()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        DateOnly dob = today.AddYears(-25);
        var data = new CodiceFiscaleData(Gender.Female, dob, "H501");
        Assert.Equal(25, data.GetAge());
    }

    [Fact]
    public void IsAdult_PersonClearlyOver18_ShouldReturnTrue()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        DateOnly dob = today.AddYears(-30).AddMonths(-1);
        var data = new CodiceFiscaleData(Gender.Male, dob, "H501");
        Assert.True(data.IsAdult());
    }

    [Fact]
    public void IsAdult_PersonClearlyUnder18_ShouldReturnFalse()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        DateOnly dob = today.AddYears(-10).AddMonths(-1);
        var data = new CodiceFiscaleData(Gender.Female, dob, "H501");
        Assert.False(data.IsAdult());
    }

    [Fact]
    public void IsAdult_PersonExactly18_BirthdayPassedLastMonth_ShouldReturnTrue()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        DateOnly dob = today.AddYears(-18).AddMonths(-1);
        var data = new CodiceFiscaleData(Gender.Male, dob, "H501");
        Assert.True(data.IsAdult());
    }

    [Fact]
    public void IsAdult_PersonAlmost18_BirthdayNextMonth_ShouldReturnFalse()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        DateOnly dob = today.AddYears(-18).AddMonths(1); // birthday next month → still 17
        var data = new CodiceFiscaleData(Gender.Female, dob, "H501");
        Assert.False(data.IsAdult());
    }

    [Fact]
    public void IsAdult_IsConsistentWithGetAge()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        DateOnly dob = today.AddYears(-20).AddMonths(-1);
        var data = new CodiceFiscaleData(Gender.Male, dob, "H501");
        Assert.Equal(data.GetAge() >= 18, data.IsAdult());
    }
}
