<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    {{$t('AddShift.UpdateShift')}}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    {{ $t('AddShift.AddShift') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">{{ $t('AddShift.ShiftName') }}: <span class="text-danger"> *</span></label>
                        <input class="form-control" v-model="shift.shiftName" type="text" />
                    </div>

                    <div class="form-group col-sm-6">
                        <label>{{ $t('AddShift.StartTime') }}</label>
                        <datepicker v-model="shift.startTime" />
                    </div>

                    <div class="form-group col-sm-6">
                        <label>{{ $t('AddShift.EndTime') }}</label>
                        <datepicker v-model="shift.endTime" />
                    </div>

                    <div class="form-group col-md-12">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="shift.status">
                            <label for="inlineCheckbox1">{{ $t('AddShift.Status') }}  </label>
                        </div>
                    </div>

                    <div class="form-group col-md-6">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox2" v-model="shift.monday">
                            <label for="inlineCheckbox2"> {{ $t('AddShift.Monday') }} </label>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox3" v-model="shift.tuesday">
                            <label for="inlineCheckbox3">{{ $t('AddShift.Tuesday') }}  </label>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox4" v-model="shift.wednesday">
                            <label for="inlineCheckbox4">{{ $t('AddShift.Wednesday') }}  </label>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox5" v-model="shift.thursday">
                            <label for="inlineCheckbox5">{{ $t('AddShift.Thursday') }}  </label>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox6" v-model="shift.friday">
                            <label for="inlineCheckbox6">{{ $t('AddShift.Friday') }}  </label>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox7" v-model="shift.saturday">
                            <label for="inlineCheckbox7">{{ $t('AddShift.Saturday') }}  </label>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox8" v-model="shift.sunday">
                            <label for="inlineCheckbox8">{{ $t('AddShift.Sunday') }}  </label>
                        </div>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('AddShift.Description') }}: <span class="text-danger"> *</span></label>
                        <textarea class="form-control" v-model="shift.description" type="text" />
                    </div>

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveShift"
                        v-bind:disabled="v$.shift.$invalid" v-if="type != 'Edit'">
                    {{
                            $t('AddShift.Save')
                    }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveShift"
                        v-bind:disabled="v$.shift.$invalid" v-if="type == 'Edit'">
                    {{
                            $t('AddShift.Update')
                    }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    {{
                        $t('AddShift.Cancel')
                    }}
                </button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
       import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    
    

    export default {
        mixins: [clickMixin],
        props: ['show', 'newshift', 'type'],
        components: {
            Loading
        },
          setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                shift: this.newshift,
                currency: '',
                arabic: '',
                english: '',
                loading: false,
            }
        },
        validations() {
           return{
             shift: {
                shiftName: {
                    required
                },
                startTime: {
                    required
                },
                endTime: {
                    required
                },
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
                this.$https.get('/Payroll/ShiftAutoGenerateNo', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.shift.code = response.data;
                    }
                });
            },

            SaveShift: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Hr/SaveShiftInformation', this.shift, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {
                            if (root.type != "Edit") {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });

                                root.close();
                            }
                            else {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
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
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: "Your Brand Name  Already Exist!",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');



        }
    }
</script>
