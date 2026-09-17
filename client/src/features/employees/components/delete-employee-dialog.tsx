import { Button } from '@/components/ui/button'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'

import type { Employee } from '../types/employee'

type DeleteEmployeeDialogProps = {
  open: boolean
  onOpenChange: (open: boolean) => void
  employee?: Employee
  isSubmitting: boolean
  errorMessage?: string | null
  onConfirm: () => void
}

export function DeleteEmployeeDialog({
  open,
  onOpenChange,
  employee,
  isSubmitting,
  errorMessage,
  onConfirm,
}: DeleteEmployeeDialogProps) {
  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle className="text-heading font-semibold">
            Delete employee
          </DialogTitle>
          <DialogDescription>
            {employee
              ? `This will permanently delete ${employee.firstName} ${employee.lastName}. This action cannot be undone.`
              : 'This action cannot be undone.'}
          </DialogDescription>
        </DialogHeader>
        {errorMessage ? (
          <p className="text-body-sm text-destructive">{errorMessage}</p>
        ) : null}
        <DialogFooter>
          <Button
            type="button"
            variant="outline"
            onClick={() => onOpenChange(false)}
          >
            Cancel
          </Button>
          <Button
            type="button"
            variant="destructive"
            disabled={isSubmitting}
            onClick={onConfirm}
          >
            {isSubmitting ? 'Deleting…' : 'Delete'}
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
