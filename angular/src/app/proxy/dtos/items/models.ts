import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateItemDto {
  itemName: string;
  itemType?: string;
  groupName?: string;
  barcode?: string;
  openingPrice: number;
  minLimit?: number | null;
  maxLimit?: number | null;
  reorderQty?: number | null;
  unitOfMeasure?: string;
  notes?: string;
  isActive?: boolean | null;
  itemNameEn?: string;
  itemTypeEn?: string;
  groupNameEn?: string;
  unitOfMeasureEn?: string;
  itemPrices: CreateItemPriceDto[];
}

export interface CreateItemPriceDto {
  unitName: string;
  wholePrice?: number | null;
  retailPrice?: number | null;
  consumerPrice?: number | null;
  currency?: string;
  effectiveDate?: string | null;
  isActive?: boolean | null;
  unitNameEn?: string;
  currencyEn?: string;
}

export interface ItemDto extends FullAuditedEntityDto<string> {
  itemCode?: string;
  itemName?: string;
  itemType?: string;
  groupName?: string;
  barcode?: string;
  openingPrice?: number;
  minLimit?: number | null;
  maxLimit?: number | null;
  reorderQty?: number | null;
  unitOfMeasure?: string;
  notes?: string;
  isActive?: boolean | null;
  itemNameEn?: string;
  itemTypeEn?: string;
  groupNameEn?: string;
  unitOfMeasureEn?: string;
  tenantId?: string | null;
  itemPrices?: ItemPriceDto[];
}

export interface ItemPagedRequestDto extends PagedAndSortedResultRequestDto {
  filter?: string | null;
  isActive?: boolean | null;
  groupName?: string | null;
  itemType?: string | null;
}

export interface ItemPriceDto extends FullAuditedEntityDto<string> {
  itemId?: string;
  unitName?: string;
  wholePrice?: number | null;
  retailPrice?: number | null;
  consumerPrice?: number | null;
  currency?: string;
  effectiveDate?: string | null;
  isActive?: boolean | null;
  unitNameEn?: string;
  currencyEn?: string;
  tenantId?: string | null;
}

export interface UpdateItemDto {
  itemName: string;
  itemType?: string;
  groupName?: string;
  barcode?: string;
  openingPrice: number;
  minLimit?: number | null;
  maxLimit?: number | null;
  reorderQty?: number | null;
  unitOfMeasure?: string;
  notes?: string;
  isActive?: boolean | null;
  itemNameEn?: string;
  itemTypeEn?: string;
  groupNameEn?: string;
  unitOfMeasureEn?: string;
  itemPrices: UpdateItemPriceDto[];
}

export interface UpdateItemPriceDto {
  id?: string;
  unitName: string;
  wholePrice?: number | null;
  retailPrice?: number | null;
  consumerPrice?: number | null;
  currency?: string;
  effectiveDate?: string | null;
  isActive?: boolean | null;
  unitNameEn?: string;
  currencyEn?: string;
}
