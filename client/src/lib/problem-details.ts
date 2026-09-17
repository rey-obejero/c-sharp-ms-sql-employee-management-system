import { isAxiosError } from 'axios'

export type ProblemDetails = {
  title?: string
  detail?: string
  errors?: Record<string, string[]>
}

export function getProblemDetails(error: unknown): ProblemDetails | null {
  if (isAxiosError(error) && error.response?.data) {
    return error.response.data as ProblemDetails
  }

  return null
}

export function getErrorMessage(error: unknown): string {
  const fallback = 'Something went wrong. Please try again.'
  const problem = getProblemDetails(error)

  if (!problem) {
    return fallback
  }

  const firstFieldError = problem.errors
    ? Object.values(problem.errors).flat()[0]
    : undefined

  return firstFieldError ?? problem.detail ?? problem.title ?? fallback
}
