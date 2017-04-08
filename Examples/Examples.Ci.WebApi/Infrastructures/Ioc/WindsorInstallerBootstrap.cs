
namespace Examples.Ci.WebApi.Infrastructures.Ioc
{
    //using Attributes;
    //using Castle.Windsor.Installer;
    //using System;
    //using System.Collections.Generic;
    //using System.Diagnostics.CodeAnalysis;
    //using System.Linq;

    //[ExcludeFromCodeCoverage]
    //public class WindsorInstallerBootstrap : InstallerFactory
    //{
    //    public override IEnumerable<Type> Select(IEnumerable<Type> installerTypes)
    //    {
    //        //return base.Select(installerTypes);

    //        return installerTypes.OrderBy(p => GetPriority(p));
    //    }

    //    private int GetPriority(Type type)
    //    {
    //        var attribute = type.GetCustomAttributes(typeof(InstallerPriorityAttribute), false).FirstOrDefault() as InstallerPriorityAttribute;
    //        return attribute != null ? attribute.Priority : InstallerPriorityAttribute.DefaultPriority;
    //    }
    //}
}