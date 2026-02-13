import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateSupplierDto {
  mainAccountId: string;
  supplierName: string;
  phone?: string | null;
  email?: string | null;
  address?: string | null;
  taxNumber?: string | null;
  notes?: string | null;
  isActive?: boolean | null;
  supplierNameEn?: string | null;
}

export interface SupplierDto extends FullAuditedEntityDto<string> {
  supplierCode?: string;
  supplierName?: string;
  subAccountId?: string | null;
  phone?: string;
  email?: string;
  address?: string;
  taxNumber?: string;
  notes?: string;
  isActive?: boolean | null;
  supplierNameEn?: string;
  tenantId?: string | null;
}

export interface SupplierPagedRequestDto extends PagedAndSortedResultRequestDto {
  filter?: string | null;
  isActive?: boolean | null;
  subAccountId?: string | null;
}

export interface UpdateSupplierDto {
  supplierName: string;
  phone?: string | null;
  email?: string | null;
  address?: string | null;
  taxNumber?: string | null;
  notes?: string | null;
  isActive?: boolean | null;
  supplierNameEn?: string | null;
}
