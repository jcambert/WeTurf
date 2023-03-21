using Blazorise;
using Blazorise.Utilities;
using Microsoft.AspNetCore.Components;

namespace We.Turf.Blazor.Components;

public partial class CourseCard:BaseComponent
{
    /// <summary>
    /// Specifies the content to be rendered inside this <see cref="Card"/>.
    /// </summary>
    [Parameter] public RenderFragment ChildContent { get; set; }

    protected override void BuildClasses(ClassBuilder builder)
    {
        //"tab left-tab pt-2 pb-3 mb-3 programme-course-card course-button";
        builder.Append("tab");
        builder.Append("tab-left");
        builder.Append("pt-2");
        builder.Append("pb-3");
        builder.Append("mb-3");
        builder.Append("programme-course-card");
        builder.Append("course-button");

        base.BuildClasses(builder);
    }
}
