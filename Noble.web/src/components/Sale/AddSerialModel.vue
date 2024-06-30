<template>
    <modal :show="show" v-if=" isValid('CanAddColor') || isValid('CanEditColor') ">

        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">
                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('SaleItem.Serial') }} </h5>
                        </div>

                        <div class="card-body ">
                            <div class="row ">
                                <div class="form-group has-label col-sm-12" v-for="(prod, index) in serialList" :key="index">
                                    <label class="text  font-weight-bolder"> {{ $t('SaleItem.Serial') }} {{index+1}}:<span class="text-danger"> *</span></label>
                                    <input class="form-control" @blur="CheckSerialExist(prod)" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="prod.serial" type="text" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer justify-content-right">
                            <button type="button" class="btn btn-primary" v-bind:disabled="serialList.filter(x => x.serial=='').length > 0" v-on:click="SaveColor"> {{ $t('AddColors.btnUpdate') }}</button>
                            <button type="button" class="btn btn-danger mr-3 " v-on:click="close()">{{ $t('AddColors.btnClear') }}</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        props: ['show', 'item'],
        mixins: [clickMixin],
        data: function () {
            return {
                arabic: '',
                english: '',
                render: 0,
                loading: false,
                isMultiUnit: false,
                serialList: [],
                itemQuantity: 0
            }
        },

        methods: {

            CheckSerialExist: function (item) {
                var root = this;
                
                if (item.serial!='') {
                    var prd = this.serialList.find(x => x.serial == item.serial && x.quantity != item.quantity);
                    if (prd != undefined) {
                        root.$swal({
                            title: 'Error',
                            text: 'Serial Already Exist In List',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                        item.serial = '';
                    }
                    else {
                        root.$https
                            .get('/Sale/CheckSerialExist?serial=' + item.serial + '&productId=' + this.item.productId, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } }).then(function (response) {

                                if (response.data == false) {
                                    root.$swal({
                                        title: 'Error',
                                        text: 'Serial Not Found',
                                        confirmButtonClass: "btn btn-danger",
                                        icon: 'error',
                                        timer: 4000,
                                        timerProgressBar: true,
                                    });
                                    item.serial = '';
                                }
                            });
                    }
                }

            },

            close: function () {
                this.$emit('close');
            },

            SaveColor: function () {
                
                var root = this;
                var serial = '';
                root.serialList.forEach(function (item) {
                    serial += item.serial + ',';
                });
                serial = serial.slice(0, -1);
                this.$emit("update:modelValue", serial, this.item);
            }

        },
        mounted: function () {
            
            var root = this;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.isMultiUnit = localStorage.getItem('IsMultiUnit') == 'true' ? true : false;
            
            if (this.item.serial != '' && this.item.serial != null && this.item.serial != undefined) {

                var myResult = this.item.serial.split(",");
                for (var i = 0; i < this.item.totalPiece; i++) {
                    root.serialList.push({
                        quantity: i + 1,
                        serial: myResult[i] == undefined ? '' : myResult[i],
                    });
                }

            }
            else {
                for (var j = 1; j <= parseInt(this.item.totalPiece); j++) {
                    root.serialList.push({
                        quantity: j,
                        serial: '',
                    });
                }
            }
        }
    }
</script>
