import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateInvoiceDetailDto {
  itemId: string;
  unitName: string;
  quantity: number;
  unitPrice: number;
  discount?: number | null;
  taxRate?: number | null;
}

export interface CreateInvoiceDto {
  invoiceType: string;
  supplierId?: string | null;
  warehouseId: string;
  currency?: string;
  totalAmount?: number | null;
  discount?: number | null;
  taxAmount?: number | null;
  paymentStatus?: string;
  notes?: string;
  invoiceDetails: CreateInvoiceDetailDto[];
}

export interface InvoiceDetailDto extends FullAuditedEntityDto<string> {
  invoiceId?: string;
  itemId?: string;
  unitName?: string;
  quantity?: number;
  unitPrice?: number;
  discount?: number | null;
  taxRate?: number | null;
  taxAmount?: number | null;
  totalBeforeTax?: number | null;
  totalWithTax?: number | null;
  tenantId?: string | null;
}

export interface InvoiceDto extends FullAuditedEntityDto<string> {
  invoiceNumber?: string;
  invoiceType?: string;
  supplierId?: string | null;
  warehouseId?: string;
  currency?: string;
  totalAmount?: number | null;
  discount?: number | null;
  netAmount?: number | null;
  taxAmount?: number | null;
  grandTotal?: number | null;
  journalEntryId?: string | null;
  paymentStatus?: string;
  notes?: string;
  tenantId?: string | null;
  invoiceDetails?: InvoiceDetailDto[];
}

export interface InvoicePagedRequestDto extends PagedAndSortedResultRequestDto {
  filter?: string | null;
  invoiceType?: string | null;
  paymentStatus?: string | null;
}

export interface UpdateInvoiceDetailDto {
  id?: string;
  itemId: string;
  unitName: string;
  quantity: number;
  unitPrice: number;
  discount?: number | null;
  taxRate?: number | null;
}

export interface UpdateInvoiceDto {
  invoiceType: string;
  supplierId?: string | null;
  warehouseId: string;
  currency?: string;
  totalAmount?: number | null;
  discount?: number | null;
  taxAmount?: number | null;
  paymentStatus?: string;
  notes?: string;
  invoiceDetails: UpdateInvoiceDetailDto[];
}
