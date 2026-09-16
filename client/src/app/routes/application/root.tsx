import { Outlet } from 'react-router'

import { RootLayout } from '@/components/layouts'

export function ApplicationRoot() {
  return (
    <RootLayout>
      <Outlet />
    </RootLayout>
  )
}
