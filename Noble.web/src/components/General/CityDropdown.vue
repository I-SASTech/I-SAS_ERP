<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" :placeholder="$t('CityDropdown.SelectCity')" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <template v-slot:noResult>
            <span  class="btn btn-primary " v-on:click="AddCity('Add')">{{ $t('CityDropdown.AddCity') }}</span><br />
               </template>
        </multiselect>

        <modal :show="show" v-if="show">

        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">

                            <h5 class="modal-title" id="myModalLabel"> {{ $t('CityDropdown.UpdateCity') }}</h5>

                        </div>
                        <div class="modal-header" v-else>

                            <h5 class="modal-title" id="myModalLabel"> {{ $t('CityDropdown.AddCity') }}</h5>

                        </div>
                        <div class="text-left">
                            <div class="card-body">
                                <div class="row ">
                                    <div :key="render" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.city.code.$error}">
                                        <label class="text  font-weight-bolder"> {{ $t('CityDropdown.Code') }}:<span class="text-danger"> *</span></label>
                                        <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.city.code.$model" type="text" />
                                        <span v-if="v$.city.code.$error" class="error">
                                            <span v-if="!v$.city.code.maxLength">{{ $t('CityDropdown.CodeLength') }}</span>
                                        </span>
                                    </div>
                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.city.name.$error}">
                                        <label class="text  font-weight-bolder"> {{ $t('CityDropdown.CityName') }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"  v-model="v$.city.name.$model" type="text" />
                                        <span v-if="v$.city.name.$error" class="error">
                                            <span v-if="!v$.city.name.required">{{ $t('CityDropdown.NameRequired') }}</span>
                                            <span v-if="!v$.city.name.maxLength">{{ $t('CityDropdown.NameLength') }}</span>
                                        </span>
                                    </div>


                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.city.description.$error}">
                                        <label class="text  font-weight-bolder"> {{ $t('CityDropdown.Description') }}: </label>
                                        <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.city.description.$model" type="text" />
                                        <span v-if="v$.city.description.$error" class="error">{{ $t('CityDropdown.descriptionLength') }}</span>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label style="margin: 7px;">{{ $t('CityDropdown.Active') }}</label> <br />
                                        <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': city.isActive, 'bootstrap-switch-off': !city.isActive}" v-on:click="city.isActive = !city.isActive" style="width: 72px;">
                                            <div class="bootstrap-switch-container" style="width: 122px; margin-left: 0px;">
                                                <span class="bootstrap-switch-handle-on bootstrap-switch-success" style="width: 50px;">
                                                    <i class="nc-icon nc-check-2"></i>
                                                </span>
                                                <span class="bootstrap-switch-label" style="width: 30px;">&nbsp;</span>
                                                <span class="bootstrap-switch-handle-off bootstrap-switch-success" style="width: 50px;">
                                                    <i class="nc-icon nc-simple-remove"></i>
                                                </span>
                                                <input class="bootstrap-switch" type="checkbox" data-toggle="switch" checked="" data-on-label="<i class='nc-icon nc-check-2'></i>" data-off-label="<i class='nc-icon nc-simple-remove'></i>" data-on-city="success" data-off-city="success">
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="modal-footer justify-content-right" v-if="type=='Edit'">

                            <button type="button" class="btn btn-primary  " v-on:click="SaveCity" v-bind:disabled="v$.city.$invalid"> {{ $t('CityDropdown.Update') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('CityDropdown.btnClear') }}</button>

                        </div>
                        <div class="modal-footer justify-content-right" v-else>

                            <button type="button" class="btn btn-primary  " v-on:click="SaveCity" v-bind:disabled="v$.city.$invalid"> {{ $t('CityDropdown.btnSave') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('CityDropdown.btnClear') }}</button>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </modal>
    </div>
</template>
<script>
    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    export default {
        name: 'citydropdown',
        props: ["modelValue"],
        mixins: [clickMixin],

        components: {
            Multiselect,
            
        },
         setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
               options: [],
               value: '',
                show: false,
                type: '',
                city: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    name: '',
                    description: '',
                    isActive: true
                },
                render: 0
            }
        },
        validations() {
          return{
              city: {
                name: {
                    required,
                    maxLength: maxLength(50)
                },
                code: {
                    maxLength: maxLength(30)
                },
                description: {
                    maxLength: maxLength(200)
                }
            }
          }
        },
        methods: {
            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Region/CityList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data != null) {
                        response.data.citys.forEach(function (cat) {
                            root.options.push({
                                id: cat.id,
                                name: cat.code + ' ' +cat.name
                            })
                       })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    })
                });
            },
            AddCity: function (type) {
                this.v$.$reset();
                this.GetAutoCodeGenerator();
                this.city = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    name: '',
                    description: '',
                    isActive: true
                }

                this.show = !this.show;
                this.type = type;
            },
            close: function () {
                this.show = false;
            },
            GetAutoCodeGenerator: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Region/CityCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data != null) {
                        root.city.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveCity: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Region/SaveCity', this.city, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                            
                            root.$swal({
                                icon: 'success',
                                title: 'Saved Successfully!',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.show = false;
                            root.getData();
                        }
                        else {
                            
                            root.$swal({
                               title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: "Your city " + response.data.city.name + " has been updated!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.show = false;
                            root.getData();
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your City Name  Already Exist!",
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                });
            }
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
            this.getData();
        },
    }
</script>