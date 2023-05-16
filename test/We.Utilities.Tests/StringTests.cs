using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We.Utilities.Tests;

public class StringTests
{
    [Theory]
    [InlineData("1", 4, '*', Padding.Both, Padding.Right, "*1**")]
    [InlineData("1", 4, '*', Padding.Both, Padding.Left, "**1*")]
    [InlineData("1", 5, '*', Padding.Both, Padding.Right, "**1**")]
    [InlineData("1", 5, '*', Padding.Both, Padding.Left, "**1**")]
    [InlineData("10", 5, '*', Padding.Both, Padding.Left, "**10*")]
    [InlineData("10", 5, '*', Padding.Both, Padding.Right, "*10**")]
    [InlineData("1", 4, '*', Padding.Left, Padding.Right, "***1")]
    [InlineData("1", 5, '*', Padding.Right, Padding.Right, "1****")]
    [InlineData("10", 4, '*', Padding.Right, Padding.Right, "10**")]
    [InlineData("10", 4, '*', Padding.Left, Padding.Right, "**10")]
    public void PaddingTests(
        string value,
        int part,
        char paddingChar,
        Padding padding,
        Padding final,
        string attendee
    )
    {
        var res0 = value.Pad(part, paddingChar, padding, final);
        Assert.Equal(attendee, res0);
    }
}
