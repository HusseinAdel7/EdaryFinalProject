import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface ChartOfAccountNodeDto {
  name?: string;
  nameEn?: string;
  accountNumber?: string;
  children?: ChartOfAccountNodeDto[];
}

export interface CreateMainAccountDto {
  accountName: string;
  accountNameEn?: string | null;
  title?: string | null;
  titleEn?: string | null;
  transferredTo?: string | null;
  transferredToEn?: string | null;
  isActive?: boolean;
  notes?: string | null;
  parentMainAccountId?: string | null;
}

export interface MainAccountDto extends FullAuditedEntityDto<string> {
  accountNumber?: string;
  accountName?: string;
  accountNameEn?: string | null;
  title?: string | null;
  titleEn?: string | null;
  transferredTo?: string | null;
  transferredToEn?: string | null;
  isActive?: boolean;
  notes?: string | null;
  parentMainAccountId?: string | null;
  tenantId?: string | null;
}

export interface MainAccountPagedRequestDto extends PagedAndSortedResultRequestDto {
  filter?: string | null;
  isActive?: boolean | null;
}

export interface UpdateMainAccountDto {
  accountName: string;
  accountNameEn?: string | null;
  title?: string | null;
  titleEn?: string | null;
  transferredTo?: string | null;
  transferredToEn?: string | null;
  isActive?: boolean;
  notes?: string | null;
  parentMainAccountId?: string | null;
}
