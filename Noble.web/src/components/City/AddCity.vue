<template>
    <modal :show="show">

        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">

                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddCity.UpdateCity') }} </h5>

                        </div>
                        <div class="modal-header" v-else>

                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddCity.AddCity') }} </h5>

                        </div>
                        <div class="">
                            <div class="card-body">
                                <div class="row ">
                                    <div :key="render" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.city.code.$error} && ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                        <label class="text  font-weight-bolder"> {{ $t('AddCity.Code') }}:<span class="text-danger"> *</span></label>
                                        <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.city.code.$model" type="text" />
                                        <span v-if="v$.city.code.$error" class="error">
                                            <span v-if="!v$.city.code.maxLength">{{ $t('AddCity.CodeLength') }}</span>
                                        </span>
                                    </div>
                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.city.name.$error} && ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                        <label class="text  font-weight-bolder"> {{ $t('AddCity.CityName') }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.city.name.$model" type="text" />
                                        <span v-if="v$.city.name.$error" class="error">
                                            <span v-if="!v$.city.name.required">{{ $t('AddCity.NameRequired') }}</span>
                                            <span v-if="!v$.city.name.maxLength">{{ $t('AddCity.NameLength') }}</span>
                                        </span>
                                    </div>


                                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.city.description.$error} && ($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                        <label class="text  font-weight-bolder"> {{ $t('AddCity.Description') }}: </label>
                                        <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.city.description.$model" type="text" />
                                        <span v-if="v$.city.description.$error" class="error">{{ $t('AddCity.descriptionLength') }}</span>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label style="margin: 7px;">{{ $t('AddCity.Active') }}</label> <br />
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

                            <button type="button" class="btn btn-primary  " v-on:click="SaveCity" v-bind:disabled="v$.city.$invalid"> {{ $t('AddCity.btnUpdate') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('AddCity.btnClear') }}</button>

                        </div>
                        <div class="modal-footer justify-content-right" v-if="type!='Edit'">

                            <button type="button" class="btn btn-primary  " v-on:click="SaveCity" v-bind:disabled="v$.city.$invalid"> {{ $t('AddCity.btnSave') }}</button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('AddCity.btnClear') }}</button>

                        </div>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'newcity', 'type'],
        mixins: [clickMixin],
        setup() {
            return { v$: useVuelidate() }
        },
         components: {
            Loading
        },
        data: function () {
            return {
                 loading: false,
                render: 0,
                city: this.newcity
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
            close: function () {
                this.$emit('close');
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
                 this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Region/SaveCity', this.city, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {
                         root.loading = false;
                        if (root.type != "Edit") {
                           
                            root.$swal({
                                title: root.$t('AddCity.Saved'),
                                text: root.$t('AddCity.SavedSuccessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });

                            root.close();
                        }
                        else {
                             root.loading = false;
                            root.$swal({
                                title: root.$t('AddCity.Saved'),
                                text: root.$t('AddCity.UpdateSucessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                    }
                    else {
                         root.loading = false;
                        root.$swal({
                            title: root.$t('AddCity.Error') ,
                            text: root.$t('AddCity.CityNameExist'),
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }
                });
            }
        },
        mounted: function () {

            if (this.city.id == '00000000-0000-0000-0000-000000000000' || this.city.id == undefined || this.city.id == '')
                this.GetAutoCodeGenerator();

        }
    }
</script>
