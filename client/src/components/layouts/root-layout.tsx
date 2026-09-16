import type { ReactNode } from 'react'

type RootLayoutProps = {
  children: ReactNode
}

export function RootLayout({ children }: RootLayoutProps) {
  return (
    <div className="min-h-svh bg-background">
      <div className="mx-auto max-w-[1440px] px-6 py-8">{children}</div>
    </div>
  )
}
