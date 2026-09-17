import { useMutation, useQueryClient } from '@tanstack/react-query'

import { employeesApi } from '../api/employees-api'
import type {
  CreateEmployeeRequest,
  UpdateEmployeeRequest,
} from '../types/employee'

export function useCreateEmployee() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: (data: CreateEmployeeRequest) => employeesApi.create(data),
    onSuccess: () => {
      void queryClient.invalidateQueries({ queryKey: ['employees'] })
    },
  })
}

export function useUpdateEmployee() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: UpdateEmployeeRequest }) =>
      employeesApi.update(id, data),
    onSuccess: () => {
      void queryClient.invalidateQueries({ queryKey: ['employees'] })
    },
  })
}
