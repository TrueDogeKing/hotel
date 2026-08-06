---
type: community
members: 42
---

# Integration Test Harness (1)

**Members:** 42 nodes

## Members
- [[.A_one_night_stay_is_an_arrival_on_one_day_and_a_departure_on_the_next()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.A_room_being_vacated_today_is_a_departure()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.A_room_occupied_through_the_day_is_left_alone()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.A_room_taken_today_by_a_group_is_an_arrival()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.Booking()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.BookingCancelled()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.BookingConfirmed()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.BookingCreated()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.Departures_and_unrelated_arrivals_on_one_day_are_separate_jobs()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.Detach()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.Each_room_of_a_multi_room_group_gets_its_own_job()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.ForDay()]] - code - src/CampCenter.Application/Services/HousekeepingPlanner.cs
- [[.Format()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.FormatDateTime()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.FormatZl()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.One_group_out_and_the_next_in_is_a_single_turnaround()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[.RemoveAssignments()]] - code - src/CampCenter.Domain/Repositories/IBookingRepository.cs
- [[.RemoveAssignments()_1]] - code - src/CampCenter.Infrastructure/Repositories/BookingRepository.cs
- [[.Stay()]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[.The_day_before_a_departure_is_not_the_departure_day()]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[Booking]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[Booking.cs]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[BookingCancelReason]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[BookingStatus_1]] - code - src/CampCenter.Domain/Entities/Booking.cs
- [[DateOnly_12]] - code
- [[DateOnly_13]] - code
- [[DateOnly_17]] - code
- [[DateOnly_33]] - code
- [[DateTime_3]] - code
- [[DateTime_5]] - code
- [[EmailMessage]] - code - src/CampCenter.Application/Interfaces/IEmailSender.cs
- [[EmailTemplates]] - code - src/CampCenter.Application/Services/EmailTemplates.cs
- [[Fact_7]] - code
- [[Guid_29]] - code
- [[Guid_61]] - code
- [[HousekeepingJob]] - code - src/CampCenter.Application/Services/HousekeepingPlanner.cs
- [[HousekeepingPlanner]] - code - src/CampCenter.Application/Services/HousekeepingPlanner.cs
- [[HousekeepingPlanner.cs]] - code - src/CampCenter.Application/Services/HousekeepingPlanner.cs
- [[HousekeepingPlannerTests]] - code - tests/CampCenter.UnitTests/Services/HousekeepingPlannerTests.cs
- [[IEnumerable]] - code
- [[List_10]] - code
- [[List_19]] - code

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Integration_Test_Harness_1
SORT file.name ASC
```

## Connections to other communities
- 15 edges to [[_COMMUNITY_CampCenter.Domain  Repositories (1)]]
- 13 edges to [[_COMMUNITY_CampCenter.UnitTests  Services (2)]]
- 13 edges to [[_COMMUNITY_Integration Test Harness (2)]]
- 9 edges to [[_COMMUNITY_Admin Booking & Notifications (1)]]
- 8 edges to [[_COMMUNITY_Public Booking Service (1)]]
- 8 edges to [[_COMMUNITY_CampCenter.Application  Services (2)]]
- 4 edges to [[_COMMUNITY_Admin Booking & Notifications (4)]]
- 3 edges to [[_COMMUNITY_ClosureService_1]]
- 3 edges to [[_COMMUNITY_Exception]]
- 3 edges to [[_COMMUNITY_components  admin]]
- 2 edges to [[_COMMUNITY_Admin Bookings Controller & DTOs]]
- 2 edges to [[_COMMUNITY_BookingConfiguration]]
- 1 edge to [[_COMMUNITY_Booking Maintenance Background Service]]
- 1 edge to [[_COMMUNITY_SmtpEmailSender]]
- 1 edge to [[_COMMUNITY_DTOs  Schedule (1)]]
- 1 edge to [[_COMMUNITY_Room Task Management (1)]]

## Top bridge nodes
- [[Booking]] - degree 80, connects to 10 communities
- [[BookingStatus_1]] - degree 10, connects to 5 communities
- [[EmailMessage]] - degree 8, connects to 3 communities
- [[.BookingCancelled()]] - degree 7, connects to 3 communities
- [[.RemoveAssignments()]] - degree 4, connects to 3 communities