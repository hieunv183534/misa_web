import Vue from 'vue'
import App from './App.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'

Vue.use(VueAxios, axios)
Vue.config.productionTip = false

//Khai báo hai eventBus để sử dụng
export const eventBus = new Vue();
export const eventBus1 = new Vue();


new Vue({
  render: h => h(App),
}).$mount('#app')


// Register a global custom directive called `v-focus`
Vue.directive('focus', {
  // When the bound element is inserted into the DOM...
  inserted: function (el) {
    // Focus the element
    el.focus()
  }
})