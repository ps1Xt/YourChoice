import React, { useEffect, useState } from 'react';
import { Container } from '@material-ui/core';
import Card from './Card';
import { Grid } from '@material-ui/core';
import PostNavMenu from './PostNavMenu';
import Pagination from '@material-ui/lab/Pagination';
import { LoadingBox } from '../Common/LoadingBox';
import { MainPageRequest } from '../../api/post/Models/MainPageRequest';
import { RequestFilters } from '../../api/grid/models/RequestFilters';
import { FilterLogicalOperators } from '../../api/grid/models/FilterLogicalOperators';
import { Filter } from '../../api/grid/models/Filter';
import { PostCard } from '../../api/post/Models/PostCard';
import { PaginatedResult } from '../../api/grid/models/PaginatedResult';
import { loadPostCards } from '../../api/post/LoadPostCards';
import { ErrorBox } from '../Common/ErrorBox';




let filter: Filter = {
  path: 'title',
  value: '',
  operation: 'contains'
}
let requestFilters: RequestFilters = {
  logicalOperator: FilterLogicalOperators.And,
  filters: [filter]
}

let defaultMainPageRequest: MainPageRequest = {
  pageSize: 8,
  pageIndex: 0,
  columnNameForSorting: 'avgRating',
  sortDirection: 'desc',
  requestFilters: requestFilters,
  section: "Home"
}




export function Main() {
  const [mainPageRequest, setMainPageRequest] = useState<MainPageRequest>(defaultMainPageRequest)
  const [lastRequest, setLastRequest] = useState("")
  const [cards, setCards] = useState<PostCard[]>([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>("")
  const [totalPages, setTotalPages] = useState(1)
  const pageHandler = (event: React.ChangeEvent<unknown>, value: number) => {
    setMainPageRequest({
      ...mainPageRequest,
      pageIndex: value - 1
    })
  }
  const calcTotalPages = (total: number, size: number): number => {
    var result = Math.ceil(total / size) 
    return result
  }
  const loadDataHandler = async () => {

    let stringRequest = JSON.stringify(mainPageRequest);
    if (stringRequest !== lastRequest) {

      try {
        setLastRequest(stringRequest)
        setLoading(true);
        setError(null)
        let paginatedResult: PaginatedResult<PostCard> = await loadPostCards(mainPageRequest, mainPageRequest.section);
        let items = paginatedResult.items;
        setCards(items)
        setTotalPages(calcTotalPages(paginatedResult.total, paginatedResult.pageSize))

      }
      catch (err) {
        setError(err.message)
        setTotalPages(0)
      }
      finally {
        setLoading(false);

      }

    }

  }

  useEffect(() => {
    loadDataHandler()
  })


  return (

    <Container style={{ margin: "40px auto" }} >
      <Grid container spacing={8}   >
        <Grid item xs={12}>
          <PostNavMenu pageRequest={[mainPageRequest, setMainPageRequest]}></PostNavMenu>
        </Grid>
        {loading && <div style={{ minHeight: '400px', minWidth: '100%', position: 'relative' }}><LoadingBox /></div>}
        {error && <div style={{ minHeight: '400px', minWidth: '100%', position: 'relative' }}><ErrorBox variant="h3" message={error} /></div>}
        {!error && !loading &&
          cards.map((card) => {
            return (
              <Grid item key={card.id} xs={12} sm={6} lg={3} >
                <Card {...card}></Card>
              </Grid>
            )
          })
        }

      </Grid>
      <Grid
        style={{ marginTop: "40px" }}
        container
        direction="row"
        justify="space-around"
        alignItems="center">
        <Pagination count={totalPages} onChange={pageHandler} color="secondary" />

      </Grid>
    </Container>

  );
}