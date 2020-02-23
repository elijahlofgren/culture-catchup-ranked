import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import TableView from './views/TableView.vue';
import MyUpVotes from './views/MyUpVotes.vue';

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/my',
      name: 'MyUpVotes',
      component: MyUpVotes
    },
    {
      path: '/table',
      name: 'TableView',
      component: TableView
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
    }
  ]
})
