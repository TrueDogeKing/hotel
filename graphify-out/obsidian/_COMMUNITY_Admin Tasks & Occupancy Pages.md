---
type: community
cohesion: 0.26
members: 12
---

# Admin Tasks & Occupancy Pages

**Cohesion:** 0.26 - loosely connected
**Members:** 12 nodes

## Members
- [[Occupancy]] - code - frontend/src/api/admin.ts
- [[OccupancyPage()]] - code - frontend/src/pages/admin/OccupancyPage.tsx
- [[OccupancyPage.tsx]] - code - frontend/src/pages/admin/OccupancyPage.tsx
- [[RoomOccupancy]] - code - frontend/src/api/admin.ts
- [[RoomTask_1]] - code - frontend/src/api/admin.ts
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
- 10 edges to [[_COMMUNITY_src  api (1)]]
- 5 edges to [[_COMMUNITY_src  utils]]
- 2 edges to [[_COMMUNITY_Frontend Icon Components]]
- 1 edge to [[_COMMUNITY_Frontend App Shell & i18n]]

## Top bridge nodes
- [[OccupancyPage.tsx]] - degree 14, connects to 4 communities
- [[TasksPage.tsx]] - degree 7, connects to 2 communities
- [[getTasks()]] - degree 4, connects to 1 community
- [[setTaskDone()]] - degree 4, connects to 1 community
- [[OccupancyPage()]] - degree 4, connects to 1 community