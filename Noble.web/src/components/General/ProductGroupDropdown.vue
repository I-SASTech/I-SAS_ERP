<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :disabled="disabled" :multiple="false"
                     v-bind:placeholder="$t('AddProductGroup.SelectProductGroup')" track-by="name" :clear-on-select="false"
                     :show-labels="false" label="name" :preselect-first="true">
            <!--<p slot="noResult" class="text-danger"> Oops! No Brand found.</p>-->
            <template v-slot:noResult>
            <a  class="btn btn-primary " v-on:click="AddBrand('Add')" v-if="isValid('CanAddBrand')">{{$t('AddProductGroup.AddProductGroup')}}</a><br />
        </template>
        </multiselect>

        <addproductgroup :newColor="newColor"
                         :show="show"
                         v-if="show"
                         @close="IsSave"
                         :type="type" />
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'product-group-dropdown',
        props: ["modelValue", 'disabled'],

        components: {
            Multiselect
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                value: '',
                show: false,
                type: '',
                newColor: {
                    id: '',
                    name: '',
                    nameArabic: '',
                    description: '',
                    code: '',
                    productList: [],
                    status: true
                },
                render: 0,
                loading: false,
            }
        },
        methods: {
            IsSave: function () {
                this.show = false;
                this.getData();
            },

            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Product/ProductGroupList?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.options = [];
                        response.data.results.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.name != '' ? result.code + ' ' + result.name : result.code + ' ' + result.nameArabic) : (result.nameArabic != '' ? result.code + ' ' + result.nameArabic : result.code + ' ' + result.name),
                            })
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },

            AddBrand: function (type) {
                this.newColor = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    name: '',
                    nameArabic: '',
                    description: '',
                    productList: [],
                    status: true
                }

                this.show = !this.show;
                this.type = type;
            },

        },
        computed: {
            DisplayValue: {
                get: function () {
                    if (this.value != "" || this.value != undefined) {
                        return this.value;
                    }
                    return this.modelValue;
                },
                set: function (value) {
                    this.value = value;
                    this.$emit('update:modelValue', value.id);
                }
            }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.getData();
        },
    }
</script>