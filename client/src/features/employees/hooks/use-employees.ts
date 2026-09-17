import { useQuery } from '@tanstack/react-query'

import { employeesApi } from '../api/employees-api'

export const employeesQueryKey = (search: string) =>
  ['employees', { search }] as const

export function useEmployees(search: string) {
  return useQuery({
    queryKey: employeesQueryKey(search),
    queryFn: () => employeesApi.list(search),
  })
}
