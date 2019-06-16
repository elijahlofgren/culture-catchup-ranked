<script src="./MovieListing.js" />
<template>
  <div class="hello">
    <h1>Culture Catchup Movie List</h1>
    <div v-if="movies">
      <v-card>
        <v-card-title>
          <v-text-field
            v-model="search"
            append-icon="search"
            label="Search"
            single-line
            hide-details
          ></v-text-field>
        </v-card-title>
        <v-data-table
          :pagination.sync="pagination"
          :rows-per-page-items="rowsPerPageItems"
          :headers="headers"
          :items="movies"
          :search="search"
        >
          <template v-slot:items="props">
            <td class="text-xs-left">
              <v-btn @click="upVote(props.item)">Upvote</v-btn>
              <v-btn @click="downVote(props.item)">Downvote</v-btn>
            </td>
            <td class="text-xs-left">{{ props.item.title }}</td>
            <td class="text-xs-left">
              <v-btn @click="searchImdb(props.item)">IMDB</v-btn>
            </td>
          </template>
          <template v-slot:no-results>
            <v-alert
              :value="true"
              color="error"
              icon="warning"
            >Your search for "{{ search }}" found no results.</v-alert>
          </template>
        </v-data-table>
      </v-card>
    </div>
  </div>
</template>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
