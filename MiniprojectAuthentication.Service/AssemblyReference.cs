using System.Reflection;

namespace MiniProjectAuthentication.Service;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}