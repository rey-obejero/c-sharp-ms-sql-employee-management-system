import { Pencil, Trash2 } from 'lucide-react'

import { Badge } from '@/components/ui/badge'
import { Button } from '@/components/ui/button'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'

import type { Employee } from '../types/employee'

const salaryFormatter = new Intl.NumberFormat('en-PH', {
  style: 'currency',
  currency: 'PHP',
  maximumFractionDigits: 0,
})

function formatDate(isoDate: string) {
  const [year, month, day] = isoDate.split('-')
  return `${month}/${day}/${year}`
}

type EmployeesTableProps = {
  employees: Employee[]
  isLoading: boolean
  isError: boolean
  onEdit: (employee: Employee) => void
  onDelete: (employee: Employee) => void
}

export function EmployeesTable({
  employees,
  isLoading,
  isError,
  onEdit,
  onDelete,
}: EmployeesTableProps) {
  return (
    <div className="overflow-hidden rounded-lg border border-border bg-card">
      <Table>
        <TableHeader>
          <TableRow className="border-border hover:bg-transparent">
            <TableHead className="h-10 px-3 font-normal text-muted-foreground">
              Name
            </TableHead>
            <TableHead className="h-10 px-3 font-normal text-muted-foreground">
              Email
            </TableHead>
            <TableHead className="h-10 px-3 font-normal text-muted-foreground">
              Phone
            </TableHead>
            <TableHead className="h-10 px-3 font-normal text-muted-foreground">
              Department
            </TableHead>
            <TableHead className="h-10 px-3 font-normal text-muted-foreground">
              Position
            </TableHead>
            <TableHead className="h-10 px-3 font-normal text-muted-foreground">
              Hire date
            </TableHead>
            <TableHead className="h-10 px-3 font-normal text-muted-foreground">
              Status
            </TableHead>
            <TableHead className="h-10 px-3 text-right font-normal text-muted-foreground">
              Salary
            </TableHead>
            <TableHead className="h-10 px-3 text-right font-normal text-muted-foreground">
              Actions
            </TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {isLoading ? (
            <TableRow className="border-border hover:bg-transparent">
              <TableCell
                colSpan={9}
                className="px-3 py-8 text-center text-body-sm text-muted-foreground"
              >
                Loading employees…
              </TableCell>
            </TableRow>
          ) : isError ? (
            <TableRow className="border-border hover:bg-transparent">
              <TableCell
                colSpan={9}
                className="px-3 py-8 text-center text-body-sm text-muted-foreground"
              >
                Could not load employees. Please try again.
              </TableCell>
            </TableRow>
          ) : employees.length === 0 ? (
            <TableRow className="border-border hover:bg-transparent">
              <TableCell
                colSpan={9}
                className="px-3 py-8 text-center text-body-sm text-muted-foreground"
              >
                No employees found.
              </TableCell>
            </TableRow>
          ) : (
            employees.map((employee) => (
              <TableRow
                key={employee.id}
                className="border-border hover:bg-muted"
              >
                <TableCell className="px-3 py-2 text-body-sm font-medium">
                  {employee.firstName} {employee.lastName}
                </TableCell>
                <TableCell className="px-3 py-2 text-body-sm">
                  {employee.email}
                </TableCell>
                <TableCell className="px-3 py-2 text-body-sm">
                  {employee.phone ?? '—'}
                </TableCell>
                <TableCell className="px-3 py-2 text-body-sm">
                  {employee.department}
                </TableCell>
                <TableCell className="px-3 py-2 text-body-sm">
                  {employee.position ?? '—'}
                </TableCell>
                <TableCell className="px-3 py-2 text-body-sm text-muted-foreground">
                  {formatDate(employee.hireDate)}
                </TableCell>
                <TableCell className="px-3 py-2">
                  <Badge
                    variant="outline"
                    className="rounded-sm font-normal text-muted-foreground"
                  >
                    {employee.status}
                  </Badge>
                </TableCell>
                <TableCell className="px-3 py-2 text-right text-body-sm">
                  {salaryFormatter.format(Number(employee.salary))}
                </TableCell>
                <TableCell className="px-3 py-2">
                  <div className="flex justify-end gap-1">
                    <Button
                      type="button"
                      variant="ghost"
                      size="icon-sm"
                      aria-label={`Edit ${employee.firstName} ${employee.lastName}`}
                      onClick={() => onEdit(employee)}
                      className="text-muted-foreground hover:bg-transparent hover:text-foreground cursor-pointer"
                    >
                      <Pencil />
                    </Button>
                    <Button
                      type="button"
                      variant="ghost"
                      size="icon-sm"
                      aria-label={`Delete ${employee.firstName} ${employee.lastName}`}
                      onClick={() => onDelete(employee)}
                      className="text-muted-foreground hover:bg-transparent hover:text-foreground cursor-pointer"
                    >
                      <Trash2 />
                    </Button>
                  </div>
                </TableCell>
              </TableRow>
            ))
          )}
        </TableBody>
      </Table>
      <div className="border-t border-border px-3 py-2 text-body-sm text-muted-foreground">
        {employees.length === 1
          ? '1 employee.'
          : `${employees.length} employees.`}
      </div>
    </div>
  )
}
