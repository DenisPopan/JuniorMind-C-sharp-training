// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S109:Magic numbers should not be used", Justification = "Only while testing", Scope = "member", Target = "~M:DiagramsProject.Program.Main")]
[assembly: SuppressMessage(
    "StyleCop.CSharp.NamingRules",
    "SA1305:Field names should not use Hungarian notation",
    Justification = "Might be a bug",
    Scope = "member",
    Target = "~M:DiagramsProject.Program.Main")]
[assembly: SuppressMessage(
    "Major Code Smell",
    "S109:Magic numbers should not be used",
    Justification = "Only while testing",
    Scope = "member",
    Target = "~M:DiagramsProject.Program.DrawRoundedRectangle(System.Drawing.Graphics,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)")]
[assembly:
    SuppressMessage(
    "Major Code Smell",
    "S109:Magic numbers should not be used",
    Justification = "testing",
    Scope = "member",
    Target = "~M:DiagramsProject.Program.DrawAndFillRoundedRectangle(System.Drawing.Graphics,System.Int32,System.Int32,System.Int32,System.Int32)")]
[assembly: SuppressMessage(
    "Major Code Smell",
    "S109:Magic numbers should not be used",
    Justification = "he",
    Scope = "member",
    Target = "~M:DiagramsProject.Program.MakeRoundedRect(System.Drawing.RectangleF,System.Single,System.Single,System.Boolean,System.Boolean,System.Boolean,System.Boolean)~System.Drawing.Drawing2D.GraphicsPath")]
[assembly: SuppressMessage("Major Code Smell", "S103:Lines should not be too long", Justification = "dsf")]
[assembly: SuppressMessage("Major Code Smell", "S109:Magic numbers should not be used", Justification = "asd", Scope = "member", Target = "~M:DiagramsProject.Program.DrawAndFillRoundedRectangle(System.Drawing.Graphics,System.Single,System.Single,System.Single,System.Single)")]
