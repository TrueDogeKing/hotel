---
type: community
cohesion: 0.26
members: 12
---

# Admin Tasks & Occupancy Pages

**Cohesion:** 0.26 - loosely connected
**Members:** 12 nodes

## Members
- [[RoomOccupancy]] - code - frontend/src/api/admin.ts
- [[RoomTask]] - code - frontend/src/api/admin.ts
- [[SessionOccupancy]] - code - frontend/src/api/admin.ts
- [[SessionOccupancyPage()]] - code - frontend/src/pages/admin/SessionOccupancyPage.tsx
- [[SessionOccupancyPage.tsx]] - code - frontend/src/pages/admin/SessionOccupancyPage.tsx
- [[TasksPage()]] - code - frontend/src/pages/admin/TasksPage.tsx
- [[TasksPage.tsx]] - code - frontend/src/pages/admin/TasksPage.tsx
- [[createTask()]] - code - frontend/src/api/admin.ts
- [[deleteTask()]] - code - frontend/src/api/admin.ts
- [[getOccupancy()]] - code - frontend/src/api/admin.ts
- [[getTasks()]] - code - frontend/src/api/admin.ts
- [[setTaskDone()]] - code - frontend/src/api/admin.ts

## Live Query (requires Dataview plugin)

```dataview
TABLE source_file, type FROM #community/Admin_Tasks__Occupancy_Pages
SORT file.name ASC
```

## Connections to other communities
- 10 edges to [[_COMMUNITY_Admin Frontend Pages]]
- 4 edges to [[_COMMUNITY_Frontend App Shell & i18n]]

## Top bridge nodes
- [[SessionOccupancyPage.tsx]] - degree 11, connects to 2 communities
- [[TasksPage.tsx]] - degree 8, connects to 2 communities
- [[getTasks()]] - degree 4, connects to 1 community
- [[setTaskDone()]] - degree 4, connects to 1 community
- [[deleteTask()]] - degree 3, connects to 1 community