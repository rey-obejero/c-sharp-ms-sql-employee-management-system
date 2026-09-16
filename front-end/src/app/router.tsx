import { createBrowserRouter, Navigate } from 'react-router'

import { Employees } from '@/app/routes/application/employees'
import { ApplicationRoot } from '@/app/routes/application/root'
import { paths } from '@/config/paths'

export const router = createBrowserRouter([
  {
    path: paths.root,
    element: <ApplicationRoot />,
    children: [
      { index: true, element: <Navigate to={paths.employees} replace /> },
      { path: paths.employees, element: <Employees /> },
    ],
  },
])
