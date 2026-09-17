import { useState } from 'react'
import type { FormEvent } from 'react'

import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'

import type {
  CreateEmployeeRequest,
  Department,
  Employee,
  EmployeeStatus,
} from '../types/employee'

const selectClassName =
  'h-9 w-full rounded-md border border-input bg-transparent px-3 text-sm shadow-xs outline-none focus-visible:border-ring focus-visible:ring-3 focus-visible:ring-ring/50'

type EmployeeFormValues = {
  firstName: string
  lastName: string
  email: string
  phone: string
  departmentId: string
  position: string
  hireDate: string
  status: EmployeeStatus
  salary: string
}

const emptyValues: EmployeeFormValues = {
  firstName: '',
  lastName: '',
  email: '',
  phone: '',
  departmentId: '',
  position: '',
  hireDate: '',
  status: 'Active',
  salary: '',
}

function toFormValues(employee: Employee): EmployeeFormValues {
  return {
    firstName: employee.firstName,
    lastName: employee.lastName,
    email: employee.email,
    phone: employee.phone ?? '',
    departmentId: String(employee.departmentId),
    position: employee.position ?? '',
    hireDate: employee.hireDate,
    status: employee.status,
    salary: String(employee.salary),
  }
}

type EmployeeFormDialogProps = {
  open: boolean
  onOpenChange: (open: boolean) => void
  departments: Department[]
  employee?: Employee
  isSubmitting: boolean
  errorMessage?: string | null
  onSubmit: (values: CreateEmployeeRequest) => void
}

export function EmployeeFormDialog({
  open,
  onOpenChange,
  departments,
  employee,
  isSubmitting,
  errorMessage,
  onSubmit,
}: EmployeeFormDialogProps) {
  const [values, setValues] = useState<EmployeeFormValues>(() =>
    employee ? toFormValues(employee) : emptyValues,
  )

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()

    onSubmit({
      firstName: values.firstName.trim(),
      lastName: values.lastName.trim(),
      email: values.email.trim(),
      phone: values.phone.trim() || null,
      departmentId: Number(values.departmentId),
      position: values.position.trim() || null,
      hireDate: values.hireDate,
      status: values.status,
      salary: Number(values.salary),
    })
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle className="text-heading font-semibold">
            {employee ? 'Edit employee' : 'Add employee'}
          </DialogTitle>
          <DialogDescription>
            {employee
              ? 'Update the details of this employee.'
              : 'Enter the details of the new employee.'}
          </DialogDescription>
        </DialogHeader>
        <form className="space-y-4" onSubmit={handleSubmit}>
          {errorMessage ? (
            <p className="text-body-sm text-destructive">{errorMessage}</p>
          ) : null}

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-1.5">
              <Label htmlFor="firstName">First name</Label>
              <Input
                id="firstName"
                value={values.firstName}
                onChange={(event) =>
                  setValues((previous) => ({
                    ...previous,
                    firstName: event.target.value,
                  }))
                }
                required
              />
            </div>
            <div className="space-y-1.5">
              <Label htmlFor="lastName">Last name</Label>
              <Input
                id="lastName"
                value={values.lastName}
                onChange={(event) =>
                  setValues((previous) => ({
                    ...previous,
                    lastName: event.target.value,
                  }))
                }
                required
              />
            </div>
          </div>

          <div className="space-y-1.5">
            <Label htmlFor="email">Email</Label>
            <Input
              id="email"
              type="email"
              value={values.email}
              onChange={(event) =>
                setValues((previous) => ({
                  ...previous,
                  email: event.target.value,
                }))
              }
              required
            />
          </div>

          <div className="space-y-1.5">
            <Label htmlFor="phone">Phone</Label>
            <Input
              id="phone"
              value={values.phone}
              onChange={(event) =>
                setValues((previous) => ({
                  ...previous,
                  phone: event.target.value,
                }))
              }
            />
          </div>

          <div className="space-y-1.5">
            <Label htmlFor="department">Department</Label>
            <select
              id="department"
              className={selectClassName}
              value={values.departmentId}
              onChange={(event) =>
                setValues((previous) => ({
                  ...previous,
                  departmentId: event.target.value,
                }))
              }
              required
            >
              <option value="" disabled>
                Select a department
              </option>
              {departments.map((department) => (
                <option key={department.id} value={department.id}>
                  {department.name}
                </option>
              ))}
            </select>
          </div>

          <div className="space-y-1.5">
            <Label htmlFor="position">Position</Label>
            <Input
              id="position"
              value={values.position}
              onChange={(event) =>
                setValues((previous) => ({
                  ...previous,
                  position: event.target.value,
                }))
              }
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-1.5">
              <Label htmlFor="hireDate">Hire date</Label>
              <Input
                id="hireDate"
                type="date"
                value={values.hireDate}
                onChange={(event) =>
                  setValues((previous) => ({
                    ...previous,
                    hireDate: event.target.value,
                  }))
                }
                required
              />
            </div>
            <div className="space-y-1.5">
              <Label htmlFor="status">Status</Label>
              <select
                id="status"
                className={selectClassName}
                value={values.status}
                onChange={(event) =>
                  setValues((previous) => ({
                    ...previous,
                    status: event.target.value as EmployeeStatus,
                  }))
                }
              >
                <option value="Active">Active</option>
                <option value="Inactive">Inactive</option>
              </select>
            </div>
          </div>

          <div className="space-y-1.5">
            <Label htmlFor="salary">Salary</Label>
            <Input
              id="salary"
              type="number"
              min={0}
              step="0.01"
              value={values.salary}
              onChange={(event) =>
                setValues((previous) => ({
                  ...previous,
                  salary: event.target.value,
                }))
              }
              required
            />
          </div>

          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => onOpenChange(false)}
            >
              Cancel
            </Button>
            <Button type="submit" disabled={isSubmitting}>
              {isSubmitting ? 'Saving…' : 'Save'}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}
