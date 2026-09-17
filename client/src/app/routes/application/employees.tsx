import { Plus } from 'lucide-react'
import { useState } from 'react'

import { Button } from '@/components/ui/button'
import { DeleteEmployeeDialog } from '@/features/employees/components/delete-employee-dialog'
import { EmployeeFormDialog } from '@/features/employees/components/employee-form-dialog'
import { EmployeesSearch } from '@/features/employees/components/employees-search'
import { EmployeesTable } from '@/features/employees/components/employees-table'
import { useDepartments } from '@/features/employees/hooks/use-departments'
import {
  useCreateEmployee,
  useDeleteEmployee,
  useUpdateEmployee,
} from '@/features/employees/hooks/use-employee-mutations'
import { useEmployees } from '@/features/employees/hooks/use-employees'
import type {
  CreateEmployeeRequest,
  Employee,
} from '@/features/employees/types/employee'
import { useDebouncedValue } from '@/hooks/use-debounced-value'
import { getErrorMessage } from '@/lib/problem-details'

export function Employees() {
  const [search, setSearch] = useState('')
  const debouncedSearch = useDebouncedValue(search)

  const [isCreateOpen, setIsCreateOpen] = useState(false)
  const [editing, setEditing] = useState<Employee | null>(null)
  const [deleting, setDeleting] = useState<Employee | null>(null)

  const employeesQuery = useEmployees(debouncedSearch)
  const departmentsQuery = useDepartments()

  const createMutation = useCreateEmployee()
  const updateMutation = useUpdateEmployee()
  const deleteMutation = useDeleteEmployee()

  const departments = departmentsQuery.data ?? []

  const handleCreate = (values: CreateEmployeeRequest) => {
    createMutation.mutate(values, {
      onSuccess: () => setIsCreateOpen(false),
    })
  }

  const handleUpdate = (values: CreateEmployeeRequest) => {
    if (!editing) {
      return
    }

    updateMutation.mutate(
      { id: Number(editing.id), data: values },
      { onSuccess: () => setEditing(null) },
    )
  }

  const handleDelete = () => {
    if (!deleting) {
      return
    }

    deleteMutation.mutate(Number(deleting.id), {
      onSuccess: () => setDeleting(null),
    })
  }

  return (
    <div className="space-y-6">
      <div className="flex items-start justify-between gap-4">
        <div className="space-y-1">
          <h1 className="text-heading font-semibold">Employees</h1>
          <p className="text-body-sm text-muted-foreground">
            View and manage your employees.
          </p>
        </div>
        <Button type="button" onClick={() => setIsCreateOpen(true)}>
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
        onEdit={setEditing}
        onDelete={setDeleting}
      />

      <EmployeeFormDialog
        key={`employee-create-${isCreateOpen}`}
        open={isCreateOpen}
        onOpenChange={setIsCreateOpen}
        departments={departments}
        isSubmitting={createMutation.isPending}
        errorMessage={
          createMutation.isError ? getErrorMessage(createMutation.error) : null
        }
        onSubmit={handleCreate}
      />

      <EmployeeFormDialog
        key={`employee-edit-${editing?.id ?? 'none'}`}
        open={editing !== null}
        onOpenChange={(open) => {
          if (!open) {
            setEditing(null)
          }
        }}
        departments={departments}
        employee={editing ?? undefined}
        isSubmitting={updateMutation.isPending}
        errorMessage={
          updateMutation.isError ? getErrorMessage(updateMutation.error) : null
        }
        onSubmit={handleUpdate}
      />

      <DeleteEmployeeDialog
        open={deleting !== null}
        onOpenChange={(open) => {
          if (!open) {
            setDeleting(null)
          }
        }}
        employee={deleting ?? undefined}
        isSubmitting={deleteMutation.isPending}
        errorMessage={
          deleteMutation.isError ? getErrorMessage(deleteMutation.error) : null
        }
        onConfirm={handleDelete}
      />
    </div>
  )
}
