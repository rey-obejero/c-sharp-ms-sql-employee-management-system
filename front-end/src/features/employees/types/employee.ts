export type EmployeeStatus = 'Active' | 'Inactive'

export type Employee = {
  id: number
  firstName: string
  lastName: string
  email: string
  phone: string
  department: string
  position: string
  hireDate: string
  status: EmployeeStatus
  salary: number
}
