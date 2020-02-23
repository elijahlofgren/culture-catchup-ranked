<script src="./MyUpVotes.js" />
<template>
  <v-card max-width="400" class="mx-auto">
    <v-system-bar color="pink darken-2" dark>
      <v-spacer></v-spacer>

      <v-icon>mdi-window-minimize</v-icon>

      <v-icon>mdi-window-maximize</v-icon>

      <v-icon>mdi-close</v-icon>
    </v-system-bar>

    <v-app-bar dark color="pink">
      <v-app-bar-nav-icon></v-app-bar-nav-icon>

      <v-toolbar-title>My Movies</v-toolbar-title>

      <v-spacer></v-spacer>

      <v-btn icon>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>
    </v-app-bar>

    <v-container>
      <v-row>

        <v-col v-for="(item, i) in movies" :key="i" cols="12">
          <v-card dark>
            <div class="movie-card d-flex flex-no-wrap justify-space-between">
              <div>
                <v-card-title class="headline" v-text="item.movie.title"></v-card-title>

                <v-card-subtitle class="sub-title" v-text="item.movieInfo.plot"></v-card-subtitle>
              </div>
              <v-avatar v-if="item.movieInfo.poster" class="ma-3" size="125" tile>
                <v-img :src="item.movieInfo.poster"></v-img>
              </v-avatar>
            </div>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-card>
</template>