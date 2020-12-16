import React, { useEffect, useState } from 'react';
import Paper from '@material-ui/core/Paper';
import {
    CustomPaging,
    DataTypeProvider,
    FilteringState,
    IntegratedFiltering,
    IntegratedPaging,
    IntegratedSorting,
    PagingState,
    SortingState,
} from '@devexpress/dx-react-grid';
import {
    Grid,
    Table,
    TableHeaderRow,
    TableFilterRow,
    PagingPanel,
} from '@devexpress/dx-react-grid-material-ui';
import { CircularProgress, createStyles, makeStyles } from '@material-ui/core';
import DateRange from '@material-ui/icons/DateRange';
import { PageRequest } from '../../api/grid/models/PageRequest';
import { RequestFilters } from '../../api/grid/models/RequestFilters';
import { Filter } from '../../api/grid/models/Filter';
import { FilterLogicalOperators } from '../../api/grid/models/FilterLogicalOperators';
import { PaginatedResult } from '../../api/grid/models/PaginatedResult';
import { Row } from '../../api/grid/models/Row';
import { LoadGridData } from '../../api/grid/LoadGridData';
import { DateToString } from '../../helpers/DateService';
import { ErrorBox } from '../Common/ErrorBox';

const useStyles = makeStyles(() =>
    createStyles({
        loadingBox: {
            position: 'absolute',
            top: 0,
            left: 0,
            width: '100%',
            height: '100%',
            background: 'rgba(255, 255, 255, .3)'
        },
        loadingIcon: {
            position: 'absolute',
            fontSize: '20px',
            top: 'calc(45% - 20px)',
            left: 'calc(50% - 20px)',
        }


    }),
);
const FilterIcon = ({ type, ...restProps }: any) => {
    if (type === 'title') return <DateRange {...restProps} />;
    return <TableFilterRow.Icon type={type} {...restProps} />;
};


export const DataGrid = () => {
    const classes = useStyles();
    const [filters, setFilters] = useState(new Array<any>());
    const [rows, setRows] = useState<Row[]>([])
    const [currentPage, setCurrentPage] = useState(0);
    const [totalCount, setTotalCount] = useState(5);
    const [pageSize] = useState(5)
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(false);
    const [notStringColumns] = useState(['favorites', 'date', 'avgRating', 'size']);
    const [stringColumns] = useState(['title']);
    const [sorting, setSorting] = useState<any>([{ columnName: 'title', direction: 'asc' }]);
    const [columnsFilteringOptions] = useState([
        'equal',
        'notEqual',
        'greaterThan',
        'greaterThanOrEqual',
        'lessThan',
        'lessThanOrEqual',
    ]);
    const [stringColumnsFilteringOptions] = useState([
        'contains',
        'notContains',
        'equal',
        'notEqual',
    ]);
    const [lastRequest, setLastRequest] = useState("")
    const [columns] = useState([
        { name: 'title', title: 'Title' },
        { name: 'favorites', title: 'Favorites' },
        { name: 'date', title: 'Date' },
        { name: 'avgRating', title: 'AvgRating' },
        { name: 'size', title: 'Size' },
    ]);


    const getPageRequest = (): PageRequest => {
        let requestFilters: RequestFilters = {
            logicalOperator: FilterLogicalOperators.And,
            filters: new Array<Filter>()
        }
        filters.forEach(filter => {
            let newFilter: Filter = {
                path: filter.columnName,
                value: filter.value,
                operation: filter.operation
            }
            requestFilters.filters.push(newFilter);
        });
        let pageRequest: PageRequest = {
            pageIndex: currentPage,
            pageSize: pageSize,
            columnNameForSorting: sorting[0].columnName,
            sortDirection: sorting[0].direction,
            requestFilters: requestFilters
        }

        return pageRequest
    };


    const loadDataHandler = async () => {

        let request = getPageRequest()
        let stringRequest = JSON.stringify(request);
        if (stringRequest !== lastRequest) {
            setLastRequest(stringRequest)
            setError(false)
            setLoading(true);
            try {
                let paginatedResult: PaginatedResult<Row> = await LoadGridData(request);
                let items = paginatedResult.items;

                setRows(items)
                setTotalCount(paginatedResult.total)
                
            }
            catch{
                setError(true)
            }
            finally{
                setLoading(false);

            }
            
        }

    }

    useEffect(() => {
        loadDataHandler()
    })
    let str = getPageRequest()
    let DateEquals: any = {
        'title': 'Equals'
    }
    return (
        <Paper style={{ position: 'relative' }}>
            <Grid
                rows={rows}
                columns={columns}

            >
                <DataTypeProvider
                    for={stringColumns}

                    availableFilterOperations={stringColumnsFilteringOptions}
                />
                <DataTypeProvider
                    for={notStringColumns}

                    availableFilterOperations={columnsFilteringOptions}
                />
                <PagingState
                    currentPage={currentPage}
                    onCurrentPageChange={setCurrentPage}
                    pageSize={pageSize}
                />
                <CustomPaging
                    totalCount={totalCount}
                />
                <SortingState
                    sorting={sorting}
                    onSortingChange={setSorting}
                />
                <FilteringState onFiltersChange={(e: any) => setFilters(e)} />
                <Table />
                <TableHeaderRow showSortingControls />
                <TableFilterRow
                    showFilterSelector
                    iconComponent={FilterIcon}
                />
                <PagingPanel />
            </Grid>
            { loading && <div className={classes.loadingBox}>
                <CircularProgress className={classes.loadingIcon} />
            </div>}
            { error &&
                
                <div>
                    <ErrorBox color="error" message="Loading Data Failed" ></ErrorBox>
                </div>
            }
        </Paper>
    );
};