import { expect, test } from '@playwright/test'

const baseURL = process.env.BASE_URL ?? 'http://127.0.0.1:8080'

test.beforeAll(async ({ request }) => {
  await expect
    .poll(
      async () => {
        const response = await request.get(`${baseURL}/api/v1/employees`)
        return response.status()
      },
      { message: 'the API should become ready', timeout: 30_000 },
    )
    .toBeLessThan(500)
})

test('employees page loads and the fetch succeeds', async ({ page }) => {
  const employeesResponse = page.waitForResponse(
    (response) =>
      response.url().includes('/api/v1/employees') &&
      response.request().method() === 'GET',
  )

  await page.goto('/employees')

  await expect(page.getByRole('heading', { name: 'Employees' })).toBeVisible()

  const response = await employeesResponse
  expect(response.ok()).toBeTruthy()

  await expect(page.getByText('juan.delacruz@example.com')).toBeVisible()
  await expect(
    page.getByText('Could not load employees. Please try again.'),
  ).toHaveCount(0)
})
