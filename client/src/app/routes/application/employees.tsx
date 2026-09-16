import { Plus } from 'lucide-react'

import { Button } from '@/components/ui/button'
import { EmployeesSearch } from '@/features/employees/components/employees-search'
import { EmployeesTable } from '@/features/employees/components/employees-table'

export function Employees() {
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
        <EmployeesSearch />
      </div>
      <EmployeesTable />
    </div>
  )
}
