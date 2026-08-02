namespace CampCenter.Api.Auth;

/// <summary>
/// Marks one endpoint as writable by a worker, against the default that every
/// write needs the Administrator role.
/// </summary>
/// <remarks>
/// An opt-in rather than an opt-out on purpose: the blanket rule in
/// <see cref="WriteRequiresAdministratorHandler"/> stays fail-closed, so an endpoint
/// added later is still administrator-only until someone writes this attribute on
/// it deliberately. Every exception to the role model is therefore one grep away.
/// </remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public sealed class AllowWorkerWriteAttribute : Attribute { }
