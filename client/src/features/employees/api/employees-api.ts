import { apiClient } from '@/lib/api-client'

import type {
  CreateEmployeeRequest,
  Department,
  Employee,
  UpdateEmployeeRequest,
} from '../types/employee'

export const employeesApi = {
  list: async (search: string): Promise<Employee[]> => {
    const response = await apiClient.get<Employee[]>('/employees', {
      params: search ? { search } : undefined,
    })

    return response.data
  },

  get: async (id: number): Promise<Employee> => {
    const response = await apiClient.get<Employee>(`/employees/${id}`)

    return response.data
  },

  create: async (data: CreateEmployeeRequest): Promise<Employee> => {
    const response = await apiClient.post<Employee>('/employees', data)

    return response.data
  },

  update: async (id: number, data: UpdateEmployeeRequest): Promise<void> => {
    await apiClient.put(`/employees/${id}`, data)
  },

  remove: async (id: number): Promise<void> => {
    await apiClient.delete(`/employees/${id}`)
  },
}

export const departmentsApi = {
  list: async (): Promise<Department[]> => {
    const response = await apiClient.get<Department[]>('/departments')

    return response.data
  },
}
