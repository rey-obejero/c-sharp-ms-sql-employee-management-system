import { Plus } from 'lucide-react'
import { useState } from 'react'

import { Button } from '@/components/ui/button'
import { EmployeesSearch } from '@/features/employees/components/employees-search'
import { EmployeesTable } from '@/features/employees/components/employees-table'
import { useEmployees } from '@/features/employees/hooks/use-employees'
import { useDebouncedValue } from '@/hooks/use-debounced-value'

export function Employees() {
  const [search, setSearch] = useState('')
  const debouncedSearch = useDebouncedValue(search)
  const employeesQuery = useEmployees(debouncedSearch)

  return (
    <div className="space-y-6">
      <div className="flex items-start justify-between gap-4">
        <div className="space-y-1">
          <h1 className="text-heading font-semibold">Employees</h1>
          <p className="text-body-sm text-muted-foreground">
            View and manage your employees.
          </p>
        </div>
        <Button type="button">
          <Plus />
          Add Employee
        </Button>
      </div>
      <div className="flex items-center justify-end">
        <EmployeesSearch value={search} onChange={setSearch} />
      </div>
      <EmployeesTable
        employees={employeesQuery.data ?? []}
        isLoading={employeesQuery.isPending}
        isError={employeesQuery.isError}
      />
    </div>
  )
}
