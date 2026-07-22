---
type: community
cohesion: 0.07
members: 39
---

# Validator Unit Tests

**Cohesion:** 0.07 - loosely connected
**Members:** 39 nodes

## Members
- [[.AfterStart()]] - code - src/CampCenter.Application/Validators/CampSessionValidators.cs
- [[.DepositAbovePrice_Fails()]] - code - tests/CampCenter.UnitTests/Validators/CampSessionValidatorsTests.cs
- [[.EndDateNotAfterStart_Fails()]] - code - tests/CampCenter.UnitTests/Validators/CampSessionValidatorsTests.cs
- [[.MissingFields_Fail()]] - code - tests/CampCenter.UnitTests/Validators/LoginRequestValidatorTests.cs
- [[.NonPositivePrice_Fails()]] - code - tests/CampCenter.UnitTests/Validators/CampSessionValidatorsTests.cs
- [[.Valid()]] - code - tests/CampCenter.UnitTests/Validators/CampSessionValidatorsTests.cs
- [[.ValidCredentials_Pass()]] - code - tests/CampCenter.UnitTests/Validators/LoginRequestValidatorTests.cs
- [[.ValidPassword()]] - code - src/CampCenter.Application/Validators/PasswordRules.cs
- [[.ValidSession_Passes()]] - code - tests/CampCenter.UnitTests/Validators/CampSessionValidatorsTests.cs
- [[AbstractValidator]] - code
- [[CampCenter.Application.Validators]] - code - src/CampCenter.Application/Validators/CampSessionValidators.cs
- [[CampCenter.UnitTests.Validators]] - code - tests/CampCenter.UnitTests/Validators/CampSessionValidatorsTests.cs
- [[CampSessionRules]] - code - src/CampCenter.Application/Validators/CampSessionValidators.cs
- [[CampSessionValidators.cs]] - code - src/CampCenter.Application/Validators/CampSessionValidators.cs
- [[CampSessionValidatorsTests]] - code - tests/CampCenter.UnitTests/Validators/CampSessionValidatorsTests.cs
- [[CampSessionValidatorsTests.cs]] - code - tests/CampCenter.UnitTests/Validators/CampSessionValidatorsTests.cs
- [[CreateBookingRequestValidator]] - code - src/CampCenter.Application/Validators/CreateBookingRequestValidator.cs
- [[CreateBookingRequestValidator.cs]] - code - src/CampCenter.Application/Validators/CreateBookingRequestValidator.cs
- [[CreateCampSessionRequestValidator]] - code - src/CampCenter.Application/Validators/CampSessionValidators.cs
- [[DateOnly_2]] - code
- [[Fact_7]] - code
- [[Fact_8]] - code
- [[Func]] - code
- [[IRuleBuilder]] - code
- [[IRuleBuilder_1]] - code
- [[IRuleBuilderOptions]] - code
- [[IRuleBuilderOptions_1]] - code
- [[InlineData]] - code
- [[InlineData_1]] - code
- [[LoginRequestValidator]] - code - src/CampCenter.Application/Validators/LoginRequestValidator.cs
- [[LoginRequestValidator.cs]] - code - src/CampCenter.Application/Validators/LoginRequestValidator.cs
- [[LoginRequestValidatorTests]] - code - tests/CampCenter.UnitTests/Validators/LoginRequestValidatorTests.cs
- [[LoginRequestValidatorTests.cs]] - code - tests/CampCenter.UnitTests/Validators/LoginRequestValidatorTests.cs
- [[PasswordRules]] - code - src/CampCenter.Application/Validators/PasswordRules.cs
- [[PasswordRules.cs]] - code - src/CampCenter.Application/Validators/PasswordRules.cs
- [[Theory]] - code
- [[Theory_1]] - code
- [[UpdateCampSessionRequestValidator]] - code - src/CampCenter.Application/Validators/CampSessionValidators.cs
- [[int]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Validator_Unit_Tests
SORT file.name ASC
```

## Connections to other communities
- 3 edges to [[_COMMUNITY_Application DTO Namespaces]]
- 3 edges to [[_COMMUNITY_Camp Session Management]]
- 3 edges to [[_COMMUNITY_Room Management]]
- 2 edges to [[_COMMUNITY_Auth DTOs & Models]]
- 1 edge to [[_COMMUNITY_Auth Controller]]
- 1 edge to [[_COMMUNITY_Public Booking Service]]

## Top bridge nodes
- [[CampCenter.Application.Validators]] - degree 7, connects to 1 community
- [[AbstractValidator]] - degree 6, connects to 1 community
- [[.Valid()]] - degree 6, connects to 1 community
- [[CampSessionValidators.cs]] - degree 5, connects to 1 community
- [[CreateCampSessionRequestValidator]] - degree 4, connects to 1 community