import { RequestFilters } from "./RequestFilters";

export interface PageRequest {
    pageIndex: number,
    pageSize: number,
    columnNameForSorting: string,
    sortDirection: string,
    requestFilters: RequestFilters
}