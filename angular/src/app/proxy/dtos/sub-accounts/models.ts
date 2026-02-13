import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateSubAccountDto {
  accountName: string;
  mainAccountId: string;
  title: string;
  accountType: string;
  creditAmount?: number | null;
  standardCreditRate: string;
  commission?: number | null;
  percentage?: number | null;
  accountCurrency: string;
  notes?: string;
  isActive?: boolean | null;
  accountNameEn?: string | null;
  titleEn?: string | null;
  accountTypeEn?: string | null;
  accountCurrencyEn?: string | null;
}

export interface SubAccountDto extends FullAuditedEntityDto<string> {
  accountNumber?: string;
  accountName?: string;
  mainAccountId?: string | null;
  title?: string;
  accountType?: string;
  creditAmount?: number | null;
  standardCreditRate?: string;
  commission?: number | null;
  percentage?: number | null;
  accountCurrency?: string;
  notes?: string;
  isActive?: boolean | null;
  accountNameEn?: string;
  titleEn?: string;
  accountTypeEn?: string;
  accountCurrencyEn?: string;
}

export interface SubAccountPagedRequestDto extends PagedAndSortedResultRequestDto {
  filter?: string | null;
  isActive?: boolean | null;
  mainAccountId?: string | null;
}

export interface UpdateSubAccountDto {
  accountName: string;
  mainAccountId: string;
  title: string;
  accountType: string;
  creditAmount?: number | null;
  standardCreditRate: string;
  commission?: number | null;
  percentage?: number | null;
  accountCurrency: string;
  notes?: string;
  isActive?: boolean | null;
  accountNameEn?: string | null;
  titleEn?: string | null;
  accountTypeEn?: string | null;
  accountCurrencyEn?: string | null;
}
