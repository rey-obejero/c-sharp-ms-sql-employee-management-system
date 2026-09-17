import { useQuery } from '@tanstack/react-query'

import { departmentsApi } from '../api/employees-api'

export const departmentsQueryKey = ['departments'] as const

export function useDepartments() {
  return useQuery({
    queryKey: departmentsQueryKey,
    queryFn: departmentsApi.list,
    staleTime: Infinity,
  })
}
