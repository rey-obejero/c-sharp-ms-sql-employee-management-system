import { Search } from 'lucide-react'

import { Input } from '@/components/ui/input'

export function EmployeesSearch() {
  return (
    <div className="relative w-full max-w-[280px]">
      <Search className="pointer-events-none absolute top-1/2 left-3 size-4 -translate-y-1/2 text-muted-foreground" />
      <Input
        type="search"
        placeholder="Search by name, email…"
        aria-label="Search employees"
        className="rounded-lg pl-9"
      />
    </div>
  )
}
