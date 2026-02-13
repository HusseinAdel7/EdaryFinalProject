import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateJournalEntryDetailDto {
  subAccountId?: string | null;
  description: string;
  debit?: number;
  credit?: number;
}

export interface CreateJournalEntryDto {
  currency: string;
  exchangeRate: number;
  notes?: string;
  currencyEn?: string;
  journalEntryDetails: CreateJournalEntryDetailDto[];
}

export interface GetJournalEntryListInput extends PagedAndSortedResultRequestDto {
  filter?: string | null;
  currency?: string | null;
  notes?: string | null;
}

export interface JournalEntryDetailDto extends FullAuditedEntityDto<string> {
  journalEntryId?: string | null;
  subAccountId?: string | null;
  description?: string;
  debit?: number;
  credit?: number;
  tenantId?: string | null;
}

export interface JournalEntryDto extends FullAuditedEntityDto<string> {
  currency?: string;
  exchangeRate?: number;
  notes?: string;
  currencyEn?: string;
  tenantId?: string | null;
  journalEntryDetails?: JournalEntryDetailDto[];
}

export interface UpdateJournalEntryDetailDto {
  id?: string;
  subAccountId?: string | null;
  description: string;
  debit?: number;
  credit?: number;
}

export interface UpdateJournalEntryDto {
  currency: string;
  exchangeRate: number;
  notes?: string;
  currencyEn?: string;
  journalEntryDetails: UpdateJournalEntryDetailDto[];
}
